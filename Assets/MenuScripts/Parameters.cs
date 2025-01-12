using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class Parameters : MonoBehaviour
{
    public PlayerSelect playerselect;
    public int numPlayers;
    public bool numPlayersChange;    
    public List<GameObject> Playerpos;
    public List<Vector3> PlayerVector;
    public List<GameObject> AllPlayers;
    public List<int> p;    
    public List<TextMeshProUGUI> infoplayer;
    public int change = 10;
    public GameObject Players;
    public GameObject x2;
    public GameObject x3;
    public GameObject x4;       
    public void leftBottom(int index)                   //cambiar de personaje a la izquierda sin repetir
    {
        bool pass = false;
        p[index]--;
        if (p[index] < 0)
        {
            p[index] = AllPlayers.Count - 1;            
        }
        while (!pass)
        {
            pass = true;
            for (int i = 0; i < p.Count; i++)
            {
                if (p[index] == p[i] && index != i)
                {
                    p[index]--;
                    if (p[index] < 0)
                    {
                        p[index] = AllPlayers.Count - 1;            
                    }  
                    pass = false;
                    break;
                }
            }
        }             
        change = index;
    }

    public void rightBottom(int index)                      //cambiar de personaje a la derecha sin repetir
    {
        bool pass = false;
        p[index]++;
        if (p[index] > AllPlayers.Count - 1)
        {
            p[index] = 0;
        }
        while (!pass)
        {
            pass = true;
            for (int i = 0; i < p.Count; i++)
            {
                if (p[index] == p[i] && index != i)
                {
                    p[index]++;
                    if (p[index] > AllPlayers.Count - 1)
                    {
                        p[index] = 0;
                    }  
                    pass = false;
                    break;
                }
            }
        }        
        change = index;
    }
    public void HandleInputData(int value)          //dropdown de numeros
    {
        numPlayers = value + 1;
        numPlayersChange = true;
    }
    public void Anterior()
    {
        SceneManager.LoadScene(0);
    }  
    public void Jugar()                         //pasar parametros al script puente y empezar el juego
    {
        playerselect.p = p;
        playerselect.numPlayers = numPlayers;
        GameObject ps = GameObject.Find("PlayerSelect");
        DontDestroyOnLoad(ps);
        SceneManager.LoadScene(3);
    }   
    void Start()
    {
        numPlayers = 1;
        x2 = GameObject.Find("2");
        x3 = GameObject.Find("3");
        x4 = GameObject.Find("4");
        x2.gameObject.SetActive(false);
        x3.gameObject.SetActive(false);
        x4.gameObject.SetActive(false);

        p = new List<int> ();
        Playerpos = new List<GameObject> ();
        AllPlayers = new List<GameObject> ();
        PlayerVector = new List<Vector3> ();

        for (int i = 0; i < Players.transform.childCount; i++)
        {            
            Players.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            AllPlayers.Add(Players.transform.GetChild(i).gameObject);
        } 
        numPlayersChange = true;             
        change = 0;                
    } 
    void FixedUpdate()
    {
        //si se cambia el numero de jugadores
        if (numPlayersChange)
        {
            for (int i = 0; i < p.Count; i++)
            {
                AllPlayers[p[i]].transform.position = new Vector3 (250,0,1);
            }
            p.Clear();
            Playerpos.Clear();
            PlayerVector.Clear();
            numPlayersChange = false;
            x2.gameObject.SetActive(false);
            x3.gameObject.SetActive(false);
            x4.gameObject.SetActive(false);
            if (numPlayers >= 1)                    //habilitacion de los campos de personaje segun la cantidad de personajes
            {
                p.Add(0);
                Playerpos.Add(GameObject.Find("playerpos1"));                
                PlayerVector.Add(new Vector3 (Playerpos[0].transform.position.x, Playerpos[0].transform.position.y, 1));
                Playerpos[0] = AllPlayers[p[0]];   
                Playerpos[0].transform.position = PlayerVector[0];             
            }            
            if (numPlayers >= 2) 
            {
                p.Add(1);                
                x2.gameObject.SetActive(true);
                Playerpos.Add(GameObject.Find("playerpos2"));
                PlayerVector.Add(new Vector3 (Playerpos[1].transform.position.x, Playerpos[1].transform.position.y, 1));
                Playerpos[1] = AllPlayers[p[1]];  
                Playerpos[1].transform.position = PlayerVector[1];              
            } 
            if (numPlayers >= 3) 
            {
                p.Add(2);                
                x3.gameObject.SetActive(true);
                Playerpos.Add(GameObject.Find("playerpos3"));
                PlayerVector.Add(new Vector3 (Playerpos[2].transform.position.x, Playerpos[2].transform.position.y, 1));
                Playerpos[2] = AllPlayers[p[2]];     
                Playerpos[2].transform.position = PlayerVector[2];           
            } 
            if (numPlayers == 4) 
            {
                p.Add(3);                
                x4.gameObject.SetActive(true);
                Playerpos.Add(GameObject.Find("playerpos4"));
                PlayerVector.Add(new Vector3 (Playerpos[3].transform.position.x, Playerpos[3].transform.position.y, 1));
                Playerpos[3] = AllPlayers[p[3]];  
                Playerpos[3].transform.position = PlayerVector[3];              
            }
        }
        //si se presiona boton izquierdo o derecho
        if (change != 10)
        {               
            Playerpos[change].transform.position = new Vector3 (250,0,1);
            Playerpos[change] = AllPlayers[p[change]];
            Playerpos[change].transform.position = PlayerVector[change];
            infoplayer[change].text = "";
            change = 10;
        }                
    }    
}
