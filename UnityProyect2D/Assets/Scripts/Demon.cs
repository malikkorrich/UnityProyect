using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    //var que representa helath  del player
    private int maxHealth;

    //var que preresenta health actual del player
    private int currentHealth;

    //objeto de tipo Healthbar
    public HealthBar healthBar;



    //var Animacion objeto responsable de la animacion 
    public Animator animator;

    //var que va referenciar posicion ataque
    public Transform posicionAtaque;

    //var rango de ataque
    public float rangoAtaque = 0.5f;

    //layer to detect enemigos
    public LayerMask enemigoLayers = 256;
    public LayerMask personajeLayer;


    //var para limitar el tiempo del ataque
    //Cuantas veces va atacar en el segundo
    public float attackRate = 1;
    //va guaradar el tiempo para atacar proxima vez
    float nextAttackTime = 0f;

    LayerMask enemigoLayer;
    public float distanciaParar = 2f;
    public float distanciaEnemigo;
    public float distanciaAliado;

    //set y get
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    public bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        //Nada mas empezar el personaje corre
        animator.SetBool("Correr", true);

        //incializar de la var health
        maxHealth = 80;
        currentHealth = maxHealth;


        //iniciar el slider o health del player 
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {


        //Pruebas raycast 
        enemigoLayer = LayerMask.GetMask("Enemigo");
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, enemigoLayer);
        //   Debug.DrawRay(transform.position + Vector3.right, Vector2.right, Color.green, 20f);
        if (hit)
        {

            //    Debug.Log("detectado: " + hit.collider.name);
            if (hit.transform.tag == "Enemigo" | hit.transform.tag == "Torre")  //  hit.collider.gameObject.layer == enemigoLayer  se puede hacer esto en lugar de comprobar el tag
            {
                //            Debug.Log("detectado enemigo: " + hit.collider.name);
                //si detectamos enemigo y todavia no estamos en distancia de ataque corremos
                animator.SetBool("Correr", true);
                //debug para ver el rayo
                //  Debug.DrawRay(transform.position + Vector3.right, Vector2.right, Color.red, 20f);
                //funcion para calcular una distancia entre dos puntos, el punto dond esta colisionado el rayo menos el punto donde esta situado el personaje 
                distanciaEnemigo = Vector2.Distance(hit.point, transform.position);
                //  Debug.Log("distancia: " + distancia);
                if (distanciaEnemigo < 2f)
                {
                    //cuando estamos en distancia de ataque paramos de correr
                    animator.SetBool("Correr", false);
                    //         Debug.Log("Distancia de ataque");
                    if (Time.time >= nextAttackTime)
                    {
                        //   animator.SetTrigger("Atacar");
                        Atacar();
                        nextAttackTime = Time.time + 1f / attackRate;


                    }

                }
            }
            else
            {

            }

            //   Debug.DrawRay(transform.position + Vector3.right , Vector2.right , Color.green,20f);


        }


        //Detecccion aliados

        personajeLayer = LayerMask.GetMask("Personaje");
        RaycastHit2D hit2;
        hit2 = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 20f, personajeLayer);

        Debug.DrawRay(transform.position + Vector3.right, Vector2.right, Color.red, 20f);


        if (hit2)
        {
            if (hit2.transform.tag == "Personaje")
            {
                //         Debug.Log("detectado aliado: " + hit2.collider.name);

                distanciaAliado = Vector2.Distance(hit2.point, transform.position);
                //       Debug.Log("distancia con aliado : " + distanciaAliado);
                if (distanciaAliado < 3f)
                {
                    animator.SetBool("Correr", false);
                    animator.SetBool("Idle", true);

                }
                else
                {
                    animator.SetBool("Correr", true);
                    animator.SetBool("Idle", false);
                }
            }



        }


        /*
                if (Input.GetKeyDown(KeyCode.C))
                {
                    animator.SetBool("Correr", true);
                    animator.SetBool("Idle", false);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    animator.SetBool("Correr", false);
                    animator.SetBool("Idle", true);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    animator.SetTrigger("Atacar");
                    animator.SetBool("Idle", true);
                    animator.SetBool("Correr", false);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Atacar", false);
                }

            */
        //Mover el personaje la derecha
        if (animator.GetBool("Correr") == true)
        {
            transform.Translate(new Vector3(0.80f, 0.0f));
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
    public void activarIdle()
    {
        if (animator.GetBool("Idle"))
        {
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
        }
    }
    public void activarCorrer()
    {
        if (animator.GetBool("Correr"))
        {
            animator.SetBool("Correr", false);
        }
        else
        {
            animator.SetBool("Correr", true);
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
        Collider2D[] hitEnemigos = Physics2D.OverlapCircleAll(posicionAtaque.position, rangoAtaque, enemigoLayers);


        //damage them
        foreach (Collider2D enemigo in hitEnemigos)
        {
            Debug.Log(" we Hit enemy:" + enemigo.name);
            //access to all enemy and damage them
            animator.SetTrigger("Atacar");
            if (enemigo.attachedRigidbody.gameObject.tag == "Enemigo")
            {
                enemigo.GetComponent<Enemigo>().takeDamage(50);
            }
            if (enemigo.attachedRigidbody.gameObject.tag == "Torre")
            {
                enemigo.GetComponent<Torre>().takeDamage(50);
            }



            /*   if(  enemigo.GetComponent<Enemigo>().healthBar.slider.value <= 0){
                     isColliding = false;
                     animator.SetBool("Correr", true);
                     animator.SetBool("Atacar", false);
                     animator.SetBool("Idle", false);

                 }*/
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*  if (collision.gameObject.tag == "Enemigo") {
             animator.SetBool("Correr", false);
             animator.SetBool("Idle", true);
             isColliding = true;


         }*/
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
}
