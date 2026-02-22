using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject home;
    public GameObject door;
    public GameObject cancel;
    public GameObject muter;
    public Image imgmuter;
    public Sprite muteon;
    public Sprite muteoff;
    public AudioSource music;
    public bool isMute = false;
    public void Jugar()
    {
        SceneManager.LoadScene(2);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Informacion()
    {
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
    public void Home()
    {
        home.SetActive(false);
        door.SetActive(true);
        cancel.SetActive(true);
        muter.SetActive(true);
    }
    public void Cancel()
    {
        home.SetActive(true);
        door.SetActive(false);
        cancel.SetActive(false);
        muter.SetActive(false);
    }
    public void Mute()
    {
        if (isMute)
        {
            imgmuter.sprite = muteoff;
            music.mute = false;
            isMute = false;
        }
        else
        {
            imgmuter.sprite = muteon;
            music.mute = true;
            isMute = true;
        }
    }
    public void Start()
    {
        home.SetActive(true);
        door.SetActive(false);
        cancel.SetActive(false);
        muter.SetActive(false);
    }
}
