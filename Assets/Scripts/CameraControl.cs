using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    public Game game;
    public GameObject playercamera;
    public GameObject maincamera;
    public GameObject player;
    public Vector3 playerpos;
    public float camspeed = 1f;
    public float playerspeed;
    
    void Start()
    {
        player = game.players[0];
        maincamera.GetComponent<Camera>().enabled = false;    
        playercamera.GetComponent<Camera>().enabled = true;
        player.transform.position = new Vector3(game.sf + 0.5f, game.sc + 0.5f, 1);
        playerspeed = 1f;
    }
    void FixedUpdate()
    {
        playerpos = player.transform.position;
        playercamera.transform.position = Vector3.Lerp(player.transform.position,playerpos, camspeed);
        /*if (Input.GetKeyDown("1"))                                                    //Libertad de movimientoo para hacer pruebas
        {
            maincamera.GetComponent<Camera>().enabled = true;
            playercamera.GetComponent<Camera>().enabled = false;
        }   
        if (Input.GetKeyDown("2"))
        {
            maincamera.GetComponent<Camera>().enabled = false;
            playercamera.GetComponent<Camera>().enabled = true;
            
        }
        /*
        if (Input.GetKeyDown("w"))
        {
            playerpos = new Vector3(playerpos.x, playerpos.y + 1, playerpos.z);
            player.transform.position = Vector3.Lerp(player.transform.position,playerpos, playerspeed);            
        } 
        if (Input.GetKeyDown("s")) 
        {
            playerpos = new Vector3(playerpos.x, playerpos.y - 1, playerpos.z);
            player.transform.position = Vector3.Lerp(player.transform.position,playerpos, playerspeed);            
        }
        if (Input.GetKeyDown("d")) 
        {
            playerpos = new Vector3(playerpos.x + 1, playerpos.y, playerpos.z);
            player.transform.position = Vector3.Lerp(player.transform.position,playerpos, playerspeed);           
        }
        if (Input.GetKeyDown("a")) 
        {
            playerpos = new Vector3(playerpos.x - 1, playerpos.y, playerpos.z);
            player.transform.position = Vector3.Lerp(player.transform.position,playerpos, playerspeed);
        }*/
    }
}
