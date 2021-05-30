using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torre : MonoBehaviour
{
    //var que representa helath  del player
    private int maxHealth;

    //var que preresenta health actual del player
    private int currentHealth;

    //objeto de tipo Healthbar
    public HealthBar healthBar;

   public  Transform reyPrefab;


    //set y get
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //incializar de la var health
        maxHealth = 100;
        currentHealth = maxHealth;


        //iniciar el slider o health del player 
        healthBar.SetMaxHealth(maxHealth);

        //   Physics2D.IgnoreCollision(reyPrefab.GetComponent<Collider2D>(), GetComponent<Collider2D>());
 //        Physics2D.IgnoreLayerCollision(9,9);

    }

    // Update is called once per frame
    void Update()
    {

        

 
    }



    //metodo para hacer daño
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        //animator.SetTrigger("Dano");

        //cuando se da daño cambiamos el valor del health o slider
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            destroyTower();
        }

    }


    private void destroyTower()
    {
        //die animation
        Debug.Log("Tower Destroyed !");
         
        //disable the enemy
        //1. disbale the component boxcolider
        GetComponent<BoxCollider2D>().enabled = false;
        //2. disbale the script
        this.enabled = false;
    }

}
