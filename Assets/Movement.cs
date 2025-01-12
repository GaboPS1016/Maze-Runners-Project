using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public selectmovecell selectcell;
    public Maze_Generator maze;
    public Game game;
    public int[,] intmaze;
    public int[,] playermaze;
    public List<int[]> posiblemoves;
    public List<int[]> way;
    public List<GameObject> posiblecells;
    public GameObject MoveCell;
    public bool cellselected = false;
    public bool timetomove = false;
    public int index;
    public int fcellselected;
    public int ccellselected;
    public int player;
    public Vector3 playerpos;
    public float playerspeed = 1f;
    public IEnumerator Move(int dado)
    {
        playerpos = game.players[player].transform.position;
        int fil = (int)playerpos.x;
        int col = (int)playerpos.y;
        int efil = maze.ef;
        int ecol = maze.ec;
        MoveCell.gameObject.SetActive(true);
        List<int[]> posiblemoves = new List<int[]>();
        playermaze = maze.PlayerMaze(fil,col);
        intmaze = maze.intmaze;
        MoveCell.GetComponent<SpriteRenderer>().enabled = true;
        if (playermaze[efil, ecol] < dado)
        {
            posiblemoves.Add(new int[] {efil,ecol});                                  
            GameObject FinalMoveCellClone = Instantiate(MoveCell, new Vector3((float)(0.5+ecol),(float)(0.5+efil),5), Quaternion.identity);
            posiblecells.Add(FinalMoveCellClone);
        }
        for (int f = 0; f < maze.large; f++)
        {
            for (int c = 0; c < maze.large; c++)
            {
                if (playermaze[f,c] == dado) 
                {
                    posiblemoves.Add(new int[] {f,c});                                  //casillas azules a las que se puede mover con el valor del dado                     
                    GameObject MoveCellClone = Instantiate(MoveCell, new Vector3((float)(0.5+f),(float)(0.5+c),5), Quaternion.identity);
                    posiblecells.Add(MoveCellClone);
                }
            }
        }
        selectcell.cells = posiblecells;
        selectcell.select = true;
        yield return new WaitUntil(() => cellselected);                                     //Esperando por la seleccion de la casilla
        cellselected = false;
        for (int e = 0; e < posiblecells.Count; e++)
        {
            Destroy(posiblecells[e]);
        }
        int[] selcell = new int[]{fcellselected, ccellselected};       
        List<int[]> way = new List<int[]> {selcell};
        int[] df = { 1, -1, 0, 0 };
        int[] dc = { 0, 0, 1, -1 };
        int num = dado;
        while (num > 0)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                int newf = fcellselected + df[dir];
                int newc = ccellselected + dc[dir];
                if (playermaze[newf, newc] == num - 1)
                {
                    way.Add(new int[] {newf, newc,});           //Creando el camino de atras para alante
                    break;
                }
            }
            num--;
        }
        way.Reverse();                                          //Invirtiendo para conseguir el camino    
        game.players[player].transform.position = Vector3.MoveTowards(game.players[player].transform.position, new Vector3(1,1,1), playerspeed);
        for (int step = 0; step < way.Count; step++)
        {
            int[] x = way[step];
            int f = x[0];
            int c = x[1];
            int moveinf = f - fil;
            int moveinc = c - col;
            fil = f;
            col = c;
            //game.players[player].transform.position = Vector3.MoveTowards(game.players[player].transform.position, new Vector3(1,1,1), playerspeed);
            //Vector3 playeripos = game.players[i].transform.position;
            //playerpos = new Vector3(playeripos.x + moveinc, playeripos.y + moveinf, playeripos.z);
            //player.transform.position = Vector3.MoveTowards(player.transform.position, playerpos, playerspeed);
            //yield return new WaitUntil(() => (int)player.transform.position.x == moveinc && (int)player.transform.position.y == moveinf);
        }
        if ((int)game.players[player].transform.position.x == efil && (int)game.players[player].transform.position.y == ecol)
        {
            game.gameFinished = true;
        }    
        game.playerMoved = true;
    }
    void FixedUpdate()
    {
        if (timetomove)
        {
            timetomove = false;
            StartCoroutine(Move(game.diceResult));
        }
    }
}
