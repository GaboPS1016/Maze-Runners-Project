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
    public GameObject player;
    public GameObject testCube;
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
                        if (i == 6 || i == 7)
                        {
                            Instantiate(testCube, new Vector3(c + 0.5f, f + 0.5f, 4.5f), Quaternion.identity);            //PRUEBA
                            continue;
                        } 
                        GameObject t = Instantiate(spritetraps[i], new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
                        if (game.intmaze[f,c] == 50) game.logs.Add(t);
                    }
                }
            }
        }
    }
    public void Penalizations()
    {
        int f = (int)game.players[moves.player].transform.position.y;
        int c = (int)game.players[moves.player].transform.position.x;
        if (game.intmaze[f, c] == 50)                       //tronco
        {
            BreakLog(f, c);
            Debug.Log("Tronco Destruido");
        }
        else if (game.intmaze[f, c] == 40)                       //portal
        {
            Portal();
            Debug.Log("Teletransportado");
        }

    }
    public void Spikes(int f, int c)
    {
        
    }
    public void Fire(int f, int c)
    {

    }
    public void BearTrap(int f, int c)
    {
        
    }
    public void Portal()
    {
        while(true)
        {
            for (int f = Random.Range(3,game.large - 5); f < game.large - 5; f++)
            {
                for (int c = Random.Range(3,game.large - 5); c < game.large - 5; c++)
                {
                    if(game.intmaze[f,c] == 0 && Random.Range(0,100) <= 30)
                    {
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
        Debug.Log("rompiendo el tronco");
        for (int i = 0; i < game.logs.Count; i++)
        {
            GameObject x = game.logs[i];
            int fx = (int)x.transform.position.y;
            int cx = (int)x.transform.position.x;
            if (f == fx && c == cx) 
            {
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
        GameObject explosionAnim = Instantiate(spritetraps[6], new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy (explosionAnim);

    }
    public IEnumerator Poison(int f, int c)
    {
        GameObject poisonAnim = Instantiate(spritetraps[7], new Vector3(c + 0.5f, f + 0.5f, 4), Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy (poisonAnim);
    }
}