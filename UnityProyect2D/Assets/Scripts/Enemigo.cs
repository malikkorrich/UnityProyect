using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour

            
{
    //var que representa helath  del player
    private int maxHealth;

    //var que preresenta health actual del player
    private int currentHealth;

    //objeto de tipo Healthbar
    public HealthBar healthBar;
    public GameObject Rey;

    public GameObject enimgo_prefab;

    //var Animacion objeto responsable de la animacion 
    public Animator animator;


    //var que va referenciar posicion ataque
    public Transform posicionAtaque;

    //var rango de ataque
    public float rangoAtaque = 0.5f;

    //layer to detect enemigos
    public LayerMask enemigoLayers;


    //var para limitar el tiempo del ataque
    //Cuantas veces va atacar en el segundo
    public float attackRate = 2f;
    //va guaradar el tiempo para atacar proxima vez
    float nextAttackTime = 0f;

     
    //var flag para mover o dejar mover 
    public bool keepMoving = true;
    LayerMask aliadoLayer;
    
    public float distanciaEnemigo;
    public float distanciaAliado;

    //set y get
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //incializar de la var health
        maxHealth = 100;
        currentHealth = maxHealth;


        //iniciar el slider o health del player 
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        //Pruebas raycast 
        aliadoLayer = LayerMask.GetMask("Personaje");
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, aliadoLayer);
    //    Debug.DrawRay(transform.position + Vector3.left, Vector2.left, Color.green, 20f);
        if (hit)
        {

      //      Debug.Log("detectado: " + hit.collider.name);
            if (hit.transform.tag == "Personaje" | hit.transform.tag == "Torre")  //  hit.collider.gameObject.layer == enemigoLayer  se puede hacer esto en lugar de comprobar el tag
            {
                //si detectamos enemigo y todavia no estamos en distancia de ataque corremos
                animator.SetBool("Correr", true);
                //debug para ver el rayo
              //  Debug.DrawRay(transform.position + Vector3.left, Vector2.left, Color.red, 20f);
                //funcion para calcular una distancia entre dos puntos, el punto dond esta colisionado el rayo menos el punto donde esta situado el personaje 
                distanciaEnemigo = Vector2.Distance(hit.point, transform.position);
                //  Debug.Log("distancia: " + distancia);
                if (distanciaEnemigo < 2f)
                {
                    //cuando estamos en distancia de ataque paramos de correr
                    animator.SetBool("Correr", false);
              //      Debug.Log("Distancia de ataque");
                    if (Time.time >= nextAttackTime)
                    {
                        //   animator.SetTrigger("Atacar");
                        Atacar();
                        nextAttackTime = Time.time + 1f / attackRate;


                    }

                }

            }
        }

        //Detecccion aliados

        aliadoLayer = LayerMask.GetMask("Enemigo");
        RaycastHit2D hit2;
        hit2 = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 20f, aliadoLayer);

     //   Debug.DrawRay(transform.position + Vector3.left, Vector2.left, Color.red, 20f);


        if (hit2)
        {
            if (hit2.transform.tag == "Enemigo")
            {
   //             Debug.Log("detectado aliado: " + hit2.collider.name);

                distanciaAliado = Vector2.Distance(hit2.point, transform.position);
      //          Debug.Log("script enemigo - distancia con aliado : " + distanciaAliado);
                if (distanciaAliado < 3f)
                {
              //      Debug.Log("script enemigo - distancia de parar");
                    animator.SetBool("Correr", false);
                    animator.SetBool("Idle", true);

                }
                else
                {
                  //  Debug.Log("script enemigo - correr ");
                    animator.SetBool("Correr", true);
                    animator.SetBool("Idle", false);
                }
            }
        }

        if (animator.GetBool("Correr") == true)
        {
            transform.Translate(new Vector3(-0.07f, 0.0f));
        }
    }



    //metodo para hacer daño
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Dano");

        //cuando se da daño cambiamos el valor del health o slider
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }

    }




    void Atacar()
    {
        animator.SetBool("Correr", false);
        animator.SetBool("Idle", true);
        //play attack animation
        // animator.SetTrigger("Atacar");
        //detect enemies in range of attack
        //se crea un circulo en la posicion de ataque que va detecter los layers de los objetos que colisiona con este circulo
        //se crea una array de colider para guardar los objetos que se han colisionado
        aliadoLayer = LayerMask.GetMask("Personaje");
        Collider2D[] hitAliados = Physics2D.OverlapCircleAll(posicionAtaque.position, rangoAtaque, aliadoLayer);


        //damage them
        foreach (Collider2D enemigo in hitAliados)
        {
        //    Debug.Log(" we Hit enemy:" + enemigo.name);
            //access to all enemy and damage them
            animator.SetTrigger("Atacar");
            if (enemigo.attachedRigidbody.gameObject.tag == "Personaje")
            {
      //          Debug.Log("detectado personaje");

                if(enemigo.attachedRigidbody.gameObject.transform.name == "Rey(Clone)" || enemigo.attachedRigidbody.gameObject.transform.name == "Rey")
                {
                //    Debug.Log("daño a rey");
                 //   Rey.GetComponent<Rey>().takeDamage(5);
                    enemigo.GetComponent<Rey>().takeDamage(50);
                }
                if (enemigo.attachedRigidbody.gameObject.transform.name == "Mago(Clone)" || enemigo.attachedRigidbody.gameObject.transform.name == "Mago")
                {
                  //  Debug.Log("daño a mago");
                    enemigo.GetComponent<Mago>().takeDamage(70);
                }
                if (enemigo.attachedRigidbody.gameObject.transform.name == "Demon(Clone)" || enemigo.attachedRigidbody.gameObject.transform.name == "Demon")
                {
                    enemigo.GetComponent<Demon>().takeDamage(5);
                }

            }

         

            if (enemigo.attachedRigidbody.gameObject.tag == "Torre")
            {
                enemigo.GetComponent<Torre>().takeDamage(5);
            }



            /*   if(  enemigo.GetComponent<Enemigo>().healthBar.slider.value <= 0){
                     isColliding = false;
                     animator.SetBool("Correr", true);
                     animator.SetBool("Atacar", false);
                     animator.SetBool("Idle", false);

                 }*/
        }

    }


    //die method
    void Die()
    {
        //die animation
        Debug.Log("Enemy Died !");
        animator.SetBool("Muerte", true);
        //disable the enemy
        //1. disbale the component boxcolider
        GetComponent<BoxCollider2D>().enabled = false;
        //2. disbale the script
        this.enabled = false;
       
    }




    public void lunchEvil()
    {
        if (DiamondCounter.valorDiamantes >= 50)
        {
            
            GameObject.Instantiate(enimgo_prefab, new Vector3(-26.74f, -12.23f, -60.69f), transform.rotation);
            DiamondCounter.valorDiamantes -= 50;
            gameObject.SetActive(true);
        }

    }


    private void OnDrawGizmosSelected()
    {
        if (rangoAtaque == null)
            return;
        Gizmos.DrawWireSphere(posicionAtaque.position, rangoAtaque);
    }
}
