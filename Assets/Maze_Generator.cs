using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Maze_Generator : MonoBehaviour
{
    camera cameraa;
    Players players; 
    Parameters parameter;  
    Game game;
    public GameObject TrueCell;    //0
    public GameObject FalseCell;   //-1
    public GameObject StartCell;   //-5
    public GameObject FinishCell;   //-20
    public int large;                          //largo y alto del laberinto
    public bool[,] boolmaze;
    public int[,] intmaze;
    public int[,] playermaze;
    public int sf;
    public int sc;
    /*
    LEYENDA:
    -1     paredes
    -5     inicio
    -10    puntas
    */        
    public void Maze(int large)
    {
        this.large = large;
        boolmaze = new bool[large, large];
        intmaze = new int[large, large];
        playermaze = new int[large, large];
        for (int a = 0; a < large; a++)
        {
            for (int b = 0; b < large; b++)
            {
                boolmaze[a, b] = false;          //iniciando todo el laberinto con paredes
                intmaze[a, b] = -1;
            }
        }
        sf = Random.Range(1, large - 1);
        if (sf % 2 == 0) sf--;

        sc = Random.Range(1, large - 1);
        if (sc % 2 == 0) sc--;

        intmaze[sf, sc] = -5;                 //casilla de inicio (aleatoria)                  
        makingWays(sf, sc);
        Puntas(intmaze);
        PlayerMaze(sf, sc);
        End();
        printCells();                        
    }  
   
    public void makingWays(int f, int c)
    {
        boolmaze[f, c] = true;
        if (intmaze[f, c] != -5) intmaze[f, c] = 0;
        //           N  S  E  O                 //direcciones
        int[] df = { 2, -2, 0, 0 };
        int[] dc = { 0, 0, 2, -2 };            
        List<int> dir = new List<int> { 0, 1, 2, 3 };            //lista del orden de cada direccion
        while (dir.Count > 0)
        {
            int randDir = Random.Range(0, dir.Count);
            int moveDir = dir[randDir];            
            dir.RemoveAt(randDir);

            int movef = f + df[moveDir];
            int movec = c + dc[moveDir];
            if (movef > 0 && movef < large - 1 && movec > 0 && movec < large - 1 && boolmaze[movef, movec] == false)
            {
                boolmaze[movef, movec] = true;                                                  //validar camino
                intmaze[movef, movec] = 0;

                boolmaze[f + df[moveDir] / 2, c + dc[moveDir] / 2] = true;            //validar camino intermedio
                intmaze[f + df[moveDir] / 2, c + dc[moveDir] / 2] = 0;

                makingWays(movef, movec);
            }
        }            
    }
    public int[,] Puntas(int[,] intmaze)                                                    //extremos del laberinto
    {
        List<int[]> noexits = new List<int[]> { };            
        int[] df = { 1, -1, 0, 0 };
        int[] dc = { 0, 0, 1, -1 };
        for (int f = 0; f < large; f++)
        {
            for (int c = 0; c < large; c++)
            {
                if (intmaze[f, c] == 0)
                {
                    int d = 0;
                    for (int dir = 0; dir < 4; dir++)
                    {
                        int newf = f + df[dir];
                        int newc = c + dc[dir];
                        if (intmaze[newf, newc] == 0 || intmaze[newf, newc] == -5 || intmaze[newf,newc] == -10)d++;
                    }
                    if (d == 1)
                    {
                        int[] par = { f, c};
                        noexits.Add(par);
                        intmaze[f,c] = -10;
                    }
                }
            }
        }
        return intmaze;
    }
    public int[,] PlayerMaze( int sf, int sc)                                //Algoritmo de Lee
    {
        
        int[] df = { 0, 0, 1, -1 };
        int[] dc = { 1, -1, 0, 0 };
        for (int f = 0; f < large; f++)
        {
            for (int c = 0; c < large; c++)
            {
                if (boolmaze[f,c]) playermaze[f,c] = 0;
                else playermaze[f,c] = -1;                                              
            }        
        }
        playermaze[sf,sc] = 1; 
        bool changes;
        do
        {
            changes = false;
            for (int f = 0; f < large; f++)
            {
                for (int c = 0; c < large; c++)
                {
                    if (playermaze[f,c] == 0 || playermaze[f,c] == -1) continue;
                    for (int dir = 0; dir < 4; dir++)
                    {                                
                        int newf = f + df[dir];
                        int newc = c + dc[dir];
                        if (boolmaze[newf, newc] && playermaze[newf, newc] == 0)
                        {
                            if (f == sf && c == sc) playermaze[newf, newc] = playermaze[f,c];
                            else playermaze[newf, newc] = playermaze[f,c] + 1;
                            changes = true;                                 
                        }
                    }                                                    
                }
            }
        }
        while(changes);
        playermaze[sf,sc] = 0; 
        return playermaze;
    }        
    public int[,] End()
    {
        int max = 0;
        int maxf = 0;
        int maxc = 0;
        for (int f = 0; f < large; f++)
        {
            for (int c = 0; c < large; c++)
            {
                if (intmaze[f, c] == -10)
                {
                    if (playermaze[f,c] > max)
                    {
                        max = playermaze[f,c];
                        maxf = f;
                        maxc = c;
                    }
                }
            }
        }
        intmaze[maxf, maxc] = -20;
        return intmaze;
    }
    public void printCells()
    {
        for (int m = 0; m<large; m++)
        {
            for (int n = 0; n<large; n++) 
            {
                if (intmaze[m,n] == -5) 
                {
                    Instantiate(StartCell, new Vector3((float)(0.5+m),(float)(0.5+n),10), Quaternion.identity);
                    continue;
                }
                if (intmaze[m,n] == -20) 
                {
                    Instantiate(FinishCell, new Vector3((float)(0.5+m),(float)(0.5+n),10), Quaternion.identity);
                    continue;
                }
                if (!boolmaze[m,n])
                {
                    Instantiate(FalseCell, new Vector3((float)(0.5+m),(float)(0.5+n),10), Quaternion.identity);
                }
                else
                {
                    Instantiate(TrueCell, new Vector3((float)(0.5+m),(float)(0.5+n),10), Quaternion.identity);
                }                
            }
        }
    }
}