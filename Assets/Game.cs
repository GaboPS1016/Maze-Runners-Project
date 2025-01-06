using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{    
    public int[,] intmaze;
    public bool[,] boolmaze;
    public Maze_Generator maze;
    
    public int sf;
    public int sc;
    
    
    void Start()
    {
        maze.Maze(25);
        intmaze = maze.intmaze;
        boolmaze = maze.boolmaze;
        sf = maze.sf;
        sc = maze.sc;
        
              
        
    }
    void Update()
    {
        
    }
}
