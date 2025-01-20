using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public selectmovecell selectcell;
    public Maze_Generator maze;
    public Game game;
    public Traps traps;
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
    public GameObject player;
    public Vector3 playerpos;
    public float playerspeed = 5f;
    public IEnumerator Move(int dado)
    {
        playerpos = player.transform.position;
        int fil = (int)playerpos.y;
        int col = (int)playerpos.x;
        int efil = maze.ff;
        int ecol = maze.fc;
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
                if (playermaze[f,c] == dado && game.intmaze[f,c] != 60) //no se puede caer en una piedra
                {
                    if(traps.TestingLog(fil, col, f, c, dado))              //no se puede pasar por despues de un tronco  
                    
                    {
                        Debug.Log("tronco!!!");        
                        continue;
                    }           
                    posiblemoves.Add(new int[] {f,c});                                  //casillas azules a las que se puede mover con el valor del dado                     
                    GameObject MoveCellClone = Instantiate(MoveCell, new Vector3((float)(0.5+c),(float)(0.5+f),5), Quaternion.identity);
                    posiblecells.Add(MoveCellClone);
                }
            }
        }
        if (posiblemoves.Count == 0) 
        {
            game.InfoText.text = "No tienes movimientos disponibles";
            game.playerMoved = true;
            yield break;
        }                 //sin jugadas
        selectcell.cells = posiblecells;
        selectcell.select = true;
        yield return new WaitUntil(() => cellselected);                                  //Esperando por la seleccion de la casilla
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
        while (num > 1)
        {
            for (int dir = 0; dir < 4; dir++)
            {
                int newf = fcellselected + df[dir];
                int newc = ccellselected + dc[dir];
                if (playermaze[newf, newc] == num - 1)
                {
                    way.Add(new int[] {newf, newc});           //Creando el camino de atras para alante
                    fcellselected = newf;
                    ccellselected = newc;
                    break;
                }
            }
            num--;

        }
        way.Reverse();                                         //Invirtiendo para conseguir el camino    
        for (int step = 0; step < way.Count; step++)
        {
            int[] x = way[step];
            int f = x[0];
            int c = x[1];
            fil = f;
            col = c;
            Vector3 nextPosition = new Vector3(c + 0.5f, f + 0.5f, 1f);

            Transform playerTransform = player.transform;
            playerTransform.position = nextPosition;
            yield return new WaitForSeconds(0.5f);
        }
        traps.Penalizations();
        if ((int)player.transform.position.y == efil && (int)player.transform.position.x == ecol)
        {
            game.gameFinished = true;
            game.InfoText.text = "Encontraste la Gema de la Fortuna";
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
