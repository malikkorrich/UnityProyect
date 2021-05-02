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
    public GameObject gameover_pn;
    public GameObject victory_pn;
    public Torre torre1;
    public Torre torre2;
    // Start is called before the first frame update
    void Start()
    {
        diamond = new DiamondCounter();
    }

    // Update is called once per frame
    void Update()
    {
       
        diamantes_txt.text = diamond.ValorDiamantes.ToString();


        //activar pantalla gameover
        if (torre1.CurrentHealth <= 0 && gameover_pn.activeSelf == false)
        {
            gameover_pn.SetActive(true);

        }
        else if (torre2.CurrentHealth <= 0 && gameover_pn.activeSelf == false) {

            victory_pn.SetActive(true);
        }


    }



    //recargar el juego
    public void Restart() {
        diamond.ValorDiamantes = 0;
        SceneManager.LoadScene("SampleScene");
        
    }
}
