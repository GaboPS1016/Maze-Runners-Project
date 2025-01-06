using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Maze_Generator maze;
    public Game game;
    public int[,] playermaze;
    public GameObject MoveCell;
    public List<int[]> PosibleMoves(int fil, int col, int dado)
    {
        
        List<int[]> posiblemoves = new List<int[]>();
        playermaze = maze.PlayerMaze(fil,col);
        for (int f = 0; f < maze.large; f++)
        {
            for (int c = 0; c < maze.large; c++)
            {
                if (playermaze[f,c] == dado) 
                {
                    posiblemoves.Add(new int[] {f,c});
                    Instantiate(MoveCell, new Vector3((float)(0.5+f),(float)(0.5+c),1), Quaternion.identity);
                }
            }
        }
        return posiblemoves;
    }
    public int Dado()
    {
        int value = Random.Range(1, 7);
        Debug.Log(value);
        return value;
    }
    void Start()
    {
        //PosibleMoves(sf, sc, dado);
    }

    void Update()
    {
        
    }
}
