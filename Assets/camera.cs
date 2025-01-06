using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class camera : MonoBehaviour
{
    public Game game;
    public GameObject playercamera;
    public GameObject maincamera;
    public GameObject player;
    public SpriteRenderer playersprite;
    public Vector3 playerpos;
    public float speedcam = 5;
    
    void Start()
    {
        maincamera.gameObject.SetActive(true);
        playercamera.gameObject.SetActive(false);
        player.transform.position = new Vector3(game.sf + 0.5f, game.sc + 0.5f, 1);
        playersprite.enabled = true;
    }
    void FixedUpdate()
    {
        playerpos = player.transform.position;
        playercamera.transform.position = Vector3.Lerp(player.transform.position,playerpos, speedcam * Time.deltaTime);
        if (Input.GetKeyDown("1"))
        {
            maincamera.gameObject.SetActive(true);
            playercamera.gameObject.SetActive(false);
        }   
        if (Input.GetKeyDown("2"))
        {
            maincamera.gameObject.SetActive(false);
            playercamera.gameObject.SetActive(true);
            
        }
        if (Input.GetKeyDown("w"))
        {
            playerpos = new Vector3(playerpos.x, playerpos.y + 1, playerpos.z);
            player.transform.position = playerpos;            
        } 
        if (Input.GetKeyDown("s")) 
        {
            playerpos = new Vector3(playerpos.x, playerpos.y - 1, playerpos.z);
            player.transform.position = playerpos;            
        }
        if (Input.GetKeyDown("d")) 
        {
            playerpos = new Vector3(playerpos.x + 1, playerpos.y, playerpos.z);
           player.transform.position = playerpos;           
        }
        if (Input.GetKeyDown("a")) 
        {
            playerpos = new Vector3(playerpos.x - 1, playerpos.y, playerpos.z);
            player.transform.position = playerpos;
        }
        
        /*if (playercamera.activeSelf)
        {
            
        }*/
    }
}
