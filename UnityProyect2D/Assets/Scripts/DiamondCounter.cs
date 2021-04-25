using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondCounter : MonoBehaviour
{

    public static int valorDiamantes = 0;
    Text Valor;
    public float timer;
    public int delay = 1;
    // Start is called before the first frame update
    void Start()
    {
        Valor = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= delay)
        {
            timer = 0f;
            valorDiamantes += 10;
        }
        Valor.text = valorDiamantes.ToString();
    }
}
