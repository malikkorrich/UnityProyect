using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    // creamos 2 var de tipo text que sea de la interface del juego
    public Text diamantes_txt;
    private DiamondCounter diamond;
 
    // Start is called before the first frame update
    void Start()
    {
        diamond = new DiamondCounter();
    }

    // Update is called once per frame
    void Update()
    {
       
        diamantes_txt.text = diamond.ValorDiamantes.ToString();
    }
}
