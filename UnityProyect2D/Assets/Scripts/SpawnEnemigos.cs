using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject evil;
    public GameObject Mago;
    public GameObject Demon;
    public GameObject Rey;
    public float timer;
    public int delay = 4;
    public float random;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        random = Random.Range(1f, 5f);
        if (timer >= delay)
        {
            timer = 0f;

            switch (random)
            {
                case float n when n >= 1f & n < 2f:
                    //    Instantiate(evil, new Vector3(13, -13f, -60f), Quaternion.identity);
                    evil.gameObject.GetComponent<EvilEnem>().lunchEvil();
                    break;
                case float n when n >= 2f & n < 3f:
                    //   Instantiate(Mago, new Vector3(13, -13f, -60f), Quaternion.identity);
                    Mago.gameObject.GetComponent<MagoEnem>().lunchMago();
                    break;
                case float n when n >= 3f & n < 4f:
                    //     Instantiate(Demon, new Vector3(13, -13f, -60f), Quaternion.identity);
                    Demon.gameObject.GetComponent<DemonEnem>().lunchDemond();
                    break;
                case float n when n >= 4f & n < 5f:
                    //       Instantiate(Rey, new Vector3(13, -13f, -60f), Quaternion.identity);
                    Rey.gameObject.GetComponent<ReyEnem>().lunchRey();
                    break;
            }
            //    Instantiate(evil, new Vector3(13, -13f, -60f), Quaternion.identity);
        }
    }
}
