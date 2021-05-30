using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreCollision : MonoBehaviour
{

    public GameObject rey;
    public GameObject torre;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(rey.transform.GetComponent<Collider2D>(), torre.transform.GetComponent<Collider2D>(),true);
    //    Physics2D.IgnoreCollision(rey.transform.GetComponent<Collider2D>(), torre.transform.GetComponent<Collider2D>()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
