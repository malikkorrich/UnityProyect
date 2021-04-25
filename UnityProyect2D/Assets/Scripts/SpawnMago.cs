using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnMago : MonoBehaviour
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
        /*      if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
              {
                  Debug.Log("Clicked");
                  Instantiate(personaje, new Vector2(-20f, -6f), Quaternion.identity);
              }
              if (Input.GetMouseButtonDown(0))
              {
                  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                  RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                  if (hit.collider != null)
                  {
                      Debug.Log("Clicked" + hit.collider.name);
                      Instantiate(personaje, new Vector2(-20f, -6f), Quaternion.identity);
                  }
              }*/
        spawnLayer = LayerMask.GetMask("Spawn");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, spawnLayer))
            {
                Debug.Log("Clicked" + hit.collider.name);
                if (hit.transform.name == "Spawn Mago")
                {
                    if (DiamondCounter.valorDiamantes >=30)
                    {

                        Instantiate(personaje, new Vector3(-17f, -11f, -60f), Quaternion.identity);
                        DiamondCounter.valorDiamantes -= 30;
                    }
                    }
            }
        }
    }

}