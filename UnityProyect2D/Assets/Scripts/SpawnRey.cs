using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnRey: MonoBehaviour
{
    public GameObject personaje;
    public LayerMask spawnLayer;

    void Start()
    {
       
    }



    // Update is called once per frame
    void Update()
    {
        //Detecta cuando se hace click sobre el cubo
        spawnLayer = LayerMask.GetMask("Spawn");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,spawnLayer))
            {
                Debug.Log("Clicked" + hit.collider.name);
                if (hit.transform.name == "Spawn Rey")
                {
                    if (DiamondCounter.valorDiamantes >= 50)
                    {

                        Instantiate(personaje, new Vector3(-17f, -11f, -60f), Quaternion.identity);
                        DiamondCounter.valorDiamantes -= 50;
                    }
                }
            }
        }
    }

}