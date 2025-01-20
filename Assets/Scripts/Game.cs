using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    public bool abilityAvaiable = false;
    public int diceResult;
    public bool diceThrown;
    private bool startGame;
    public bool playerMoved = false;
    private bool gamming = false;
    public bool gameFinished = false;
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI AvailableText;
    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI VictoryText;
    public List<GameObject> logs;
    public int iactual;
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
                playerTurnText.text = "Jugador "+ (i+1);
                cameracontrol.player = players[i];
                movement.player = players[i];
                traps.player = players[i];
                iactual = i;
                Debug.Log("F: " + players[i].transform.position.y  + "  C: " + players[i].transform.position.x);

                if (playersInfo[i].timeToSpecial > 1) AvailableText.text = "Disponible en " + playersInfo[i].timeToSpecial + " turnos";     //Disponibilidad de la habilidad
                else if (playersInfo[i].timeToSpecial == 1) AvailableText.text = "Disponible en " + playersInfo[i].timeToSpecial + " turno";   
                else AvailableText.text = "DISPONIBLE";  
                abilityText.text = playersInfo[i].Ability;

                if (playersInfo[i].sleepTime > 0) 
                {
                    InfoText.text = "Incapaz de moverte, vuelves a la normalidad en " + playersInfo[i].sleepTime + " turnos";           //tiempo de inmovilidad
                    playersInfo[i].sleepTime--;
                    if (playersInfo[i].timeToSpecial > 0) playersInfo[i].timeToSpecial--;
                    yield return new WaitForSeconds(1);
                    InfoText.text = "";
                    continue;
                }
                dice.throwDice = true;
                yield return new WaitUntil(() => diceThrown);
                diceThrown = false;
                dice.throwDice = false;
                if (playersInfo[i].damaged) 
                {
                    InfoText.text = "Estás herido, este turno caminarás 1 casilla";
                    diceResult = 1;
                    playersInfo[i].damaged = false;
                }

                if (playersInfo[i].timeToSpecial == 0) abilityAvaiable = true;
                movement.timetomove = true;
                yield return new WaitUntil(() => playerMoved);
                abilityAvaiable = false;
                playerMoved = false;
                if (playersInfo[i].timeToSpecial > 0) playersInfo[i].timeToSpecial--;   
                if (playersInfo[i].burning > 0) playersInfo[i].burning--;
                if (playersInfo[i].burning == 0) players[p[i]].GetComponent<SpriteRenderer>().color = Color.white;
                if (gameFinished)                                                       //Juego terminado
                {
                    VictoryText.gameObject.SetActive(true);
                    VictoryText.text = "GANASTE JUGADOR " + (i+1);
                    break;
                }
                yield return new WaitForSeconds(1);
                InfoText.text = "";
            }
        }        
    }
    public void OnMouseDown()                           //Habilidad especial
    {
        if (abilityAvaiable)
        {
            abilityAvaiable = false;
            InfoText.text = "Habilidad usada";
            playersInfo[iactual].special();
            playersInfo[iactual].timeToSpecial = playersInfo[iactual].rechargeTime;
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
            players.Add(Players.transform.GetChild(p[i]).gameObject);
        }

        playersInfo = new Players[numPlayers];
        for (int i = 0; i < numPlayers; i++)
        {
            if (p[i] == 0)
            {
                var fumador = ScriptableObject.CreateInstance<Fumador>();
                fumador.Initialize(this, Players);
                fumador.player = players[i];
                playersInfo[i] = fumador;
            }
            else if (p[i] == 1)
            {
                var misterioso = ScriptableObject.CreateInstance<Misterioso>();
                misterioso.Initialize(this, Players);
                misterioso.player = players[i];
                playersInfo[i] = misterioso;
            }
            else if (p[i] == 2)
            {
                var bateador = ScriptableObject.CreateInstance<Bateador>();
                bateador.Initialize(this, Players);
                bateador.player = players[i];
                playersInfo[i] = bateador;
            }
            else if (p[i] == 3)
            {
                var maga = ScriptableObject.CreateInstance<Maga>();
                maga.Initialize(this, Players);
                maga.player = players[i];
                playersInfo[i] = maga;
            }
            else if (p[i] == 4)
            {
                var mercenario = ScriptableObject.CreateInstance<Mercenario>();
                mercenario.Initialize(this, Players);
                mercenario.player = players[i];
                playersInfo[i] = mercenario;
            }
            else if (p[i] == 5)
            {
                var skater = ScriptableObject.CreateInstance<Skater>();
                skater.Initialize(this, Players);
                skater.player = players[i];
                playersInfo[i] = skater;
            }
            else if (p[i] == 6)
            {
                var cyborg = ScriptableObject.CreateInstance<Cyborg>();
                cyborg.Initialize(this, Players);
                cyborg.player = players[i];
                playersInfo[i] = cyborg;
            }
            else
            {
                var trovador = ScriptableObject.CreateInstance<Trovador>();
                trovador.Initialize(this, Players);
                trovador.player = players[i];
                playersInfo[i] = trovador;
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