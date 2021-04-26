using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject enemigo;
    public float timer;
    public int delay = 6;
    public float random;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            timer = 0f;
         // Para cuando se metan mas enemigos xd  random = Random.Range(1f, 4f);
            Instantiate(enemigo, new Vector3(13, -13f, -60f), Quaternion.identity);
        }
    }
}
