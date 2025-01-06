using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Maze_Generator maze;
    public int[,] playermaze;
    public Players(int f, int c, int move)
    {
        playermaze = maze.PlayerMaze(f,c);

    }
}
