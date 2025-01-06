using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Parameters : MonoBehaviour
{
    public Game game;
    public void Anterior()
    {
        SceneManager.LoadScene(0);
    }  
    public void Jugar()
    {
        SceneManager.LoadScene(3);
    }    
}
