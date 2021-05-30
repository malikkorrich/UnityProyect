using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    private  AudioSource menu_audiosource;
    public AudioClip startbtn_audioclip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menu_audiosource = GetComponent<AudioSource>();
    }


    public void Play() {
        menu_audiosource.clip = startbtn_audioclip;
        menu_audiosource.Play();
        SceneManager.LoadScene("SampleScene");

    }

     public void Exit() {
        Application.Quit();
    } 
}
