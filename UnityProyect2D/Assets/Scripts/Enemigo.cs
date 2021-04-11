using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour

            
{

    //var que representa num de vidas del player
    private int health;
    //set y get
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //incializar de la var health
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {

        //Mover el personaje la derecha
        transform.Translate(new Vector3(-0.1f, 0.0f));
    }
}
