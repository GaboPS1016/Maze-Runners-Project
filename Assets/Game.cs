using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{    
    public CameraControl cameracontrol;
    public Movement movement;
    public Dado dice;
    public GameObject Players;
    public int[,] intmaze;
    public bool[,] boolmaze;
    public Maze_Generator maze;
    public int numPlayers;
    public List<GameObject> players;   
    public List<int> p;
    public GameObject pHolder;
    public int sf;
    public int sc;
    public int diceResult;
    public bool diceThrown;
    private bool startGame;
    public bool playerMoved = false;
    private bool gamming = false;
    public bool gameFinished = false;
    public void SpawnPlayers()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            players[i].GetComponent<SpriteRenderer>().enabled = true;
            players[i].transform.position = new Vector3(sf + 0.5f, sc + 0.5f, 1);
        }
        cameracontrol.player = players[0];
    }
    public IEnumerator turns()
    {
        while (!gameFinished)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                Debug.Log("Turno del jugador " + (i+1));
                cameracontrol.player = players[i];
                movement.player = p[i];
                dice.throwDice = true;
                yield return new WaitUntil(() => diceThrown);
                diceThrown = false;
                dice.throwDice = false;
                Debug.Log("el jugador " + (i+1) + " debe caminar " + diceResult + " casillas");                
                movement.timetomove = true;
                yield return new WaitUntil(() => playerMoved);
                playerMoved = false;
                if (gameFinished)
                {
                    //Cartelito de que gano el player i+1
                    Debug.Log("GANASTE JUGADOR " + (i+1));
                    break;
                }
            }
        }        
    }
    
    void Start()
    {
        pHolder = GameObject.Find("PlayerSelect");

        maze.Maze(25);
        intmaze = maze.intmaze;
        boolmaze = maze.boolmaze;
        sf = maze.sf;
        sc = maze.sc;
        numPlayers = PlayerSelect.Instance.numPlayers;
        p = PlayerSelect.Instance.p;
        players = new List<GameObject>();
        for (int i = 0; i < p.Count; i++)
        {
            players.Add(Players.transform.GetChild(i).gameObject);
        }

        startGame = true;
        
              
        
    }
    void Update()
    {
        if (startGame) 
        {
            SpawnPlayers();
            startGame = false;
            gamming = true;
        }        
        if (gamming)
        {
            gamming = false;
            StartCoroutine(turns());
            
        }
        if (Input.GetKeyDown("right")) cameracontrol.player = players[1];
        if (Input.GetKeyDown("left")) cameracontrol.player = players[0];
    }
}
