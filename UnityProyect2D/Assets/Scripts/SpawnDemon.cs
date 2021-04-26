using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemon : MonoBehaviour
{
    public GameObject personaje;
    public LayerMask spawnLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnLayer = LayerMask.GetMask("Spawn");
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, spawnLayer))
            {
                Debug.Log("Clicked" + hit.collider.name);
                if (hit.transform.name == "Spawn Demon")
                {
                    if (DiamondCounter.valorDiamantes >= 70)
                    {

                        Instantiate(personaje, new Vector3(-17f, -13f, -60f), Quaternion.identity);
                        DiamondCounter.valorDiamantes -= 70;
                    }
                }
            }
        }
    }
}
