using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public Game game;    
    public Maze_Generator maze;
    public GameObject TrapsFolder;
    public Movement moves;
    public int[,] playermaze;
    public List<int> numtraps;
    public List<GameObject> spritetraps;
    public int percent = 5;
    public GameObject exp;                  //bloque de prueba
    public GameObject ven;                  //bloque de prueba
    public GameObject player;
    public AudioSource expSound;
    public AudioSource btSound;
    public AudioSource portalSound;
    public AudioSource poiSound;
    public AudioSource logSound;
    public AudioSource fireSound;
    public AudioSource spikesSound;
    public void MakingTraps()
    {
        
        numtraps = new List<int> { 10, 20, 30, 40, 50, 60, 70, 80 };        //LEYENDA:   Sp, F, B, Port, L, R, E, P 
        spritetraps = new List<GameObject> ();
        for ( int i = 0; i < numtraps.Count; i++ )
        {
            spritetraps.Add (TrapsFolder.transform.GetChild(i).gameObject);        //GameObjects de las trampas
        }
        for ( int i = 0; i < numtraps.Count; i++)
        {
            for (int f = 1; f < game.large - 1; f++)
            {
                for (int c = 0; c < game.large - 1; c++)
                {
                    int rand = Random.Range(0, 100);
                    if (game.intmaze[f,c] == 0 && rand <= percent)
                    {
                        game.intmaze[f,c] = numtraps[i];
                        //if (i == 6) Instantiate(exp, new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
                        
                        if (i == 6 || i == 7) continue;
                        GameObject t = Instantiate(spritetraps[i], new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
                        if (game.intmaze[f,c] == 50) game.logs.Add(t);
                    }
                }
            }
        }
    }
    public void Penalizations()
    {
        int f = (int)moves.player.transform.position.y;
        int c = (int)moves.player.transform.position.x;
        if (game.intmaze[f, c] == 50)                       //tronco
        {
            BreakLog(f, c);
            game.InfoText.text = "Has talado el tronco";
        }               
        else if (game.intmaze[f, c] == 40)                       //portal
        {
            Portal();
            game.InfoText.text = "Has entrado a un portal";
        }
        if(game.inmunity) return; 
        if (game.intmaze[f, c] == 10)                           //pinchos
        {
            Spikes();
            game.InfoText.text = "Has caído en una trampa de pinchos, te has herido las piernas";
        }
        else if (game.intmaze[f, c] == 20)                      //fuego
        {
            Fire(f, c);
            game.InfoText.text = "Te has quemado en una trampa de fuego";
        }
        else if (game.intmaze[f, c] == 30)                      //trampa para osos
        {
            BearTrap();
            game.InfoText.text = "Has caído en una trampa para osos, te costará librate dos turnos";
        }
        else if (game.intmaze[f, c] == 70)                       //explosion
        {
            StartCoroutine(Explosion(f, c));           
        }
        else if (game.intmaze[f, c] == 80)                       //veneno
        {
            StartCoroutine(Poison(f, c));           
        }
    }
    public void Spikes()
    {
        spikesSound.Play();
        game.playersInfo[game.iactual].damaged = true;
    }
    public void Fire(int f, int c)
    {
        fireSound.Play();
        playermaze = maze.PlayerMaze(f, c);
        game.playersInfo[game.iactual].player.GetComponent<SpriteRenderer>().color = Color.black;
        game.playersInfo[game.iactual].burning = 3;
        for (int fil = 1; fil < game.large - 2; fil++)
        {
            for (int col = 1; col < game.large - 2; col++)
            {
                if (game.intmaze[fil, col] != 60)       //no rocas
                {
                    int n = 3;
                    while(n > 0)
                    {
                        if (playermaze[fil, col] == n && Random.Range(0,100) >= 85)
                        {
                            game.players[game.iactual].transform.position = new Vector3(col + 0.5f, fil + 0.5f, 1);
                            Penalizations();
                            return;
                        }
                        n--;
                    }
                }
            }
        }

    }
    public void BearTrap()
    {
        btSound.Play();
        game.playersInfo[game.iactual].sleepTime += 2;
    }
    public void Portal()
    {
        while(true)
        {
            for (int f = Random.Range(1,game.large - 1); f < game.large - 1; f++)
            {
                for (int c = Random.Range(1,game.large - 1); c < game.large - 1; c++)
                {
                    if(game.intmaze[f,c] == 0 && Random.Range(0,100) <= 30)
                    {
                        portalSound.Play();
                        player.transform.position = new Vector3(c + 0.5f, f + 0.5f, 1);
                        return;
                    }	
                }
            }  
        }
    }
    public bool TestingLog(int pf, int pc, int dadf, int dadc, int dado)
    {
        int f = dadf;          
        int c = dadc;        
        playermaze = maze.PlayerMaze(pf, pc);
        int[] df = { 1, -1, 0, 0 };
        int[] dc = { 0, 0, 1, -1 };
        int num = dado;
        while (num > 1)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                int newf = f + df[dir];
                int newc = c + dc[dir];
                if (playermaze[newf, newc] == num - 1)
                {
                    if (game.intmaze[newf, newc] == 50)         return true;           //Comprobando si hay tronco entre el dado y el player
                    f = newf;
                    c = newc;
                    break;
                }
            }
            num--;
        }       
        return false;
    }
    public void BreakLog(int f, int c)
    {
        for (int i = 0; i < game.logs.Count; i++)
        {
            GameObject x = game.logs[i];
            int fx = (int)x.transform.position.y;
            int cx = (int)x.transform.position.x;
            if (f == fx && c == cx) 
            {
                logSound.Play();
                Destroy(x);
                game.logs.RemoveAt(i);
            }            
        }
        game.intmaze[f,c] = 0;
    }
    public void Rock()
    {
        //Esta trampa no tiene codigo(Puesta en Movement)
    }
    public IEnumerator Explosion(int f, int c)
    {

        List<int> afected = new List<int>();
        playermaze = maze.PlayerMaze(f, c);
        for (int fil = 1; fil < game.large - 1; fil++)
        {
            for (int col = 1; col < game.large - 1; col++)
            {
                if (playermaze[fil, col] == 0 || playermaze[fil, col] == 1 || playermaze[fil, col] == 2)
                {
                    for (int i = 0; i < game.numPlayers; i++)
                    {
                        if ((int)game.players[i].transform.position.y == fil && (int)game.players[i].transform.position.x == col) 
                        {
                            afected.Add(i);
                        }
                    }
                }
            }
        }
        expSound.Play();
        GameObject explosionAnim = Instantiate(spritetraps[6], new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
        game.InfoText.text = "Bombaa!!, tú y los jugadores cercanos han reaparecido en la casilla de salida";
        yield return new WaitForSeconds(1.9f);          //tiempo de la animacion
        for (int i = 0; i < afected.Count; i++)
        {
            game.players[afected[i]].transform.position = new Vector3(game.sc + 0.5f, game.sf + 0.5f, 1);
        }
        Destroy (explosionAnim);
        game.intmaze[f, c] = 0;

    }
    public IEnumerator Poison(int f, int c)
    {
        playermaze = maze.PlayerMaze(f, c);
        for (int fil = 1; fil < game.large - 1; fil++)
        {
            for (int col = 1; col < game.large - 1; col++)
            {
                if (playermaze[fil, col] == 0 || playermaze[fil, col] == 1 || playermaze[fil, col] == 2)
                {
                    for (int i = 0; i < game.numPlayers; i++)
                    {
                        GameObject currentPlayer = game.players[i];
                        if ((int)currentPlayer.transform.position.y == fil && (int)currentPlayer.transform.position.x == col) 
                        {
                            game.playersInfo[i].timeToSpecial += 3;
                        }
                    }
                }
            }
        }
        poiSound.Play();
        GameObject poisonAnim = Instantiate(spritetraps[7], new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
        game.InfoText.text = "Veneno!!, tu habilidad especial y la de jugadores cercanos demorará en cargarse 3 turnos más";
        yield return new WaitForSeconds(3.2f);          //tiempo de la animacion
        Destroy (poisonAnim);
        game.intmaze[f, c] = 0;      
    }
}