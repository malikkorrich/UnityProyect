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
        maxHealth = 200;
        currentHealth = maxHealth;


        //iniciar el slider o health del player 
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
 

        //Mover el personaje la derecha
        transform.Translate(new Vector3(-0.2f, 0.0f));
    }


    //metodo para hacer daño
    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        //cuando se da daño cambiamos el valor del health o slider
        healthBar.SetHealth(currentHealth);

    }


}
