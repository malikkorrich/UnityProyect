using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour



{
    //objeto de tipo silder que va permitir incrementar y decrementar valor del health
    public Slider slider;

    //metodo que permite iniciar el valor del slider que es health
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;

        //para comprobar que valor del slider inicia con el tamaño del parametro helth
        slider.value = health;
    }




    //metodo que permite cambiar el valor de slide que es health 
    public void setHealth(int health)
    {

        slider.value = health;
    }
}
