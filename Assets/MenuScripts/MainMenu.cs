using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(2);
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
}
