using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Playerspace;

public class Game : MonoBehaviour
{    
    public CameraControl cameracontrol;
    public selectmovecell selectmovecell;
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
    public bool multiway;
    public bool loaded;
    public int numPlayers;
    public List<GameObject> players;   
    public List<int> p;
    public GameObject pHolder;
    public List<GameObject> chamanamarcs;
    public GameObject chamanaMarc;
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
    public bool repeatTurn = false;
    public bool gameFinished = false;
    public bool newdice = false;
    public bool inmunity = false;
    public GameObject endButton;
    public TextMeshProUGUI abilityText;
    public TextMeshProUGUI AvailableText;
    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI VictoryText;
    public int iactual;
    public AudioSource maga;
    public AudioSource trovador;
    public AudioSource bateador;
    public AudioSource cyborg;
    public AudioSource mercenario;
    public AudioSource misterioso;
    public AudioSource skater;
    public AudioSource fumador;
    public AudioSource asesino;
    public AudioSource chamana;
    public AudioSource aplausos;
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
        bool firstcicle = true;
        while (!gameFinished)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                if (firstcicle)
                {
                    firstcicle = false;                    
                    i = iactual - 1;
                    continue;
                }

                playerTurnText.text = "Jugador "+ (i+1);
                cameracontrol.player = players[i];
                movement.player = players[i];
                traps.player = players[i];
                iactual = i;

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
                if (newdice) diceThrown = true;
                else dice.throwDice = true;
                yield return new WaitUntil(() => diceThrown);
                
                diceThrown = false;
                dice.throwDice = false;
                if (playersInfo[i].damaged) 
                {
                    InfoText.text = "Estás herido, en este turno caminarás 1 casilla";
                    diceResult = 1;
                    playersInfo[i].damaged = false;
                }
                if (newdice)
                {
                    newdice = false;
                    diceResult = playersInfo[i].dicevalue;
                } 
                if (playersInfo[i].timeToSpecial == 0) abilityAvaiable = true;
                movement.timetomove = true;
                yield return new WaitUntil(() => playerMoved);
                
                abilityAvaiable = false;
                playerMoved = false;
                if (repeatTurn)
                {
                    repeatTurn = false;
                    i--;
                    continue;
                } 
                if (playersInfo[i].timeToSpecial > 0) playersInfo[i].timeToSpecial--;   
                if (playersInfo[i].burning > 0) playersInfo[i].burning--;
                if (playersInfo[i].burning == 0) playersInfo[i].player.GetComponent<SpriteRenderer>().color = Color.white;
                inmunity = false;
                if (gameFinished)                                                       //Juego terminado
                {
                    aplausos.Play();
                    VictoryText.gameObject.SetActive(true);
                    VictoryText.text = "GANASTE JUGADOR " + (i+1);
                    endButton.gameObject.SetActive(true);
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
        loaded = false;
        endButton.gameObject.SetActive(false);
        pHolder = GameObject.Find("PlayerSelect");
        GameObject datos = GameObject.Find("datos");
        if (pHolder == null)
        {
            loaded = true;
            intmaze = DatosCarga.Instance.intmaze;
            boolmaze = DatosCarga.Instance.boolmaze;
            numPlayers = DatosCarga.Instance.numplayers;
            p = DatosCarga.Instance.p;
            large = intmaze.GetLength(0);
            sf = DatosCarga.Instance.start[0];
            sc = DatosCarga.Instance.start[1];
            ff = DatosCarga.Instance.gema[0];
            fc = DatosCarga.Instance.gema[1];
            maze.printCells(intmaze, boolmaze);
            traps.OnlyPlaceTraps();
            iactual = DatosCarga.Instance.iactual;
        }
        else
        {
            if (datos != null) Destroy(datos);
            large = PlayerSelect.Instance.large;
            multiway = PlayerSelect.Instance.multicaminos;
            numPlayers = PlayerSelect.Instance.numPlayers;
            p = PlayerSelect.Instance.p;
            maze.Maze(large);
            intmaze = maze.intmaze;
            boolmaze = maze.boolmaze;
            sf = maze.sf;
            sc = maze.sc;
            ff = maze.ff;
            fc = maze.fc;
            traps.MakingTraps();
            iactual = 0;
        }
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
                fumador.Initialize(this, selectmovecell, maze, movement, Players);
                fumador.player = players[i];
                playersInfo[i] = fumador;
            }
            else if (p[i] == 1)
            {
                var misterioso = ScriptableObject.CreateInstance<Misterioso>();
                misterioso.Initialize(this, selectmovecell, maze, movement, Players);
                misterioso.player = players[i];
                playersInfo[i] = misterioso;
            }
            else if (p[i] == 2)
            {
                var bateador = ScriptableObject.CreateInstance<Bateador>();
                bateador.Initialize(this, selectmovecell, maze, movement, Players);
                bateador.player = players[i];
                playersInfo[i] = bateador;
            }
            else if (p[i] == 3)
            {
                var maga = ScriptableObject.CreateInstance<Maga>();
                maga.Initialize(this, selectmovecell, maze, movement, Players);
                maga.player = players[i];
                playersInfo[i] = maga;
            }
            else if (p[i] == 4)
            {
                var mercenario = ScriptableObject.CreateInstance<Mercenario>();
                mercenario.Initialize(this, selectmovecell, maze, movement, Players);
                mercenario.player = players[i];
                playersInfo[i] = mercenario;
            }
            else if (p[i] == 5)
            {
                var skater = ScriptableObject.CreateInstance<Skater>();
                skater.Initialize(this, selectmovecell, maze, movement, Players);
                skater.player = players[i];
                playersInfo[i] = skater;
            }
            else if (p[i] == 6)
            {
                var cyborg = ScriptableObject.CreateInstance<Cyborg>();
                cyborg.Initialize(this, selectmovecell, maze, movement, Players);
                cyborg.player = players[i];
                playersInfo[i] = cyborg;
            }
            else if (p[i] == 7)
            {
                var trovador = ScriptableObject.CreateInstance<Trovador>();
                trovador.Initialize(this, selectmovecell, maze, movement, Players);
                trovador.player = players[i];
                playersInfo[i] = trovador;
            }
            else if (p[i] == 8)
            {
                var asesino = ScriptableObject.CreateInstance<Asesino>();
                asesino.Initialize(this, selectmovecell, maze, movement, Players);
                asesino.player = players[i];
                playersInfo[i] = asesino;
            }
            else
            {
                var chamana = ScriptableObject.CreateInstance<Chamana>();
                chamana.Initialize(this, selectmovecell, maze, movement, Players);
                chamana.player = players[i];
                playersInfo[i] = chamana;
            }
        }
        startGame = true;  
    }
    void FixedUpdate()
    {
        if (startGame)
        {
            startGame = false;
            if (loaded)
            {
                List<int> timetospe = DatosCarga.Instance.timetospecial;
                List<int> sleep = DatosCarga.Instance.sleeptime;
                List<int> burn = DatosCarga.Instance.burning;
                List<bool> dams = DatosCarga.Instance.damageds;

                for (int i = 0; i < numPlayers; i++)
                {
                    players[i].GetComponent<SpriteRenderer>().enabled = true;

                    playersInfo[i].timeToSpecial = timetospe[i];
                    playersInfo[i].sleepTime = sleep[i];
                    playersInfo[i].burning = burn[i];
                    if (burn[i] > 0) playersInfo[i].player.GetComponent<SpriteRenderer>().color = Color.black;
                    playersInfo[i].damaged = dams[i];
                }
                List<Vector3> vectors = DatosCarga.Instance.positions;
                for (int j = 0; j < numPlayers; j++)
                {
                    float x = vectors[j].x;
                    float y = vectors[j].y;
                    float z = vectors[j].z;
                    players[j].transform.position = new Vector3(x, y, z);
                }   
            }
            else SpawnPlayers();
            loaded = false;
            gamming = true;
        }        
        if (gamming)
        {
            gamming = false;
            StartCoroutine(turns());
        }
    }
}