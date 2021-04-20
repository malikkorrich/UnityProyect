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

        //limitar el tiempo de ataque
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                Atacar();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }


        while (keepMoving) {
        //Mover el personaje la derecha
        transform.Translate(new Vector3(-0.5f, 0.0f));
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
        //play attack animation
        animator.SetTrigger("Atacar");
        //detect enemies in range of attack
        //se crea un circulo en la posicion de ataque que va detecter los layers de los objetos que colisiona con este circulo
        //se crea una array de colider para guardar los objetos que se han colisionado
        Collider2D[] hitEnemigos = Physics2D.OverlapCircleAll(posicionAtaque.position, rangoAtaque, enemigoLayers);


        //damage them
        foreach (Collider2D enemigo in hitEnemigos)
        {
            Debug.Log(" we Hit enemy:" + enemigo.name);
            //access to all enemy and damage them
            enemigo.GetComponent<Personaje>().takeDamage(100);


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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Personaje")
        {
            keepMoving = false; 
        }
    }






    private void OnDrawGizmosSelected()
    {
        if (rangoAtaque == null)
            return;
        Gizmos.DrawWireSphere(posicionAtaque.position, rangoAtaque);
    }
}
