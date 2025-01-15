using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playerspace;

public class Game : MonoBehaviour
{    
    public CameraControl cameracontrol;
    public Traps traps;
    public int large;
    public Movement movement;
    public Dado dice;
    public Players playersclass;
    public GameObject Players;
    public int[,] intmaze;
    public bool[,] boolmaze;
    public Players[] playersInfo;
    public Maze_Generator maze;
    public int numPlayers;
    public List<GameObject> players;   
    public List<int> p;
    public GameObject pHolder;
    public int sf;
    public int sc;
    public int ff;
    public int fc;
    public int diceResult;
    public bool diceThrown;
    private bool startGame;
    public bool playerMoved = false;
    private bool gamming = false;
    public bool gameFinished = false;
    public List<GameObject> logs;
    public void SpawnPlayers()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            players[i].GetComponent<SpriteRenderer>().enabled = true;
            players[i].transform.position = new Vector3(sc + 0.5f, sf + 0.5f, 1);
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
                traps.player = players[p[i]];
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
        large = 25;
        maze.Maze(large);
        intmaze = maze.intmaze;
        boolmaze = maze.boolmaze;
        sf = maze.sf;
        sc = maze.sc;
        ff = maze.ff;
        fc = maze.fc;
        logs =new List<GameObject> ();
        traps.MakingTraps();
        numPlayers = PlayerSelect.Instance.numPlayers;
        p = PlayerSelect.Instance.p;
        players = new List<GameObject>();
        for (int i = 0; i < p.Count; i++)
        {
            players.Add(Players.transform.GetChild(i).gameObject);
        }
        
        playersInfo = new Players[numPlayers];
        for (int i = 0; i < numPlayers; i++)
        {
            if (p[i] == 0)
            {
                var fumador = ScriptableObject.CreateInstance<Fumador>();
                fumador.Initialize(this, Players);
                playersInfo[i] = fumador;
            }
            
                        
        }
        startGame = true;      
        
    }
    void FixedUpdate()
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
    }
}
