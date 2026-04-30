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
    public bool antilogs = false;
    public int index;
    public int fcellselected;
    public int ccellselected;
    public GameObject player;
    public Vector3 playerpos;
    public IEnumerator Move(int dado)
    {
        playerpos = player.transform.position;
        int fil = (int)playerpos.y;
        int col = (int)playerpos.x;
        int efil = game.ff;
        int ecol = game.fc;
        MoveCell.gameObject.SetActive(true);
        List<int[]> posiblemoves = new List<int[]>();
        playermaze = maze.PlayerMaze(game.boolmaze, fil, col);
        intmaze = game.intmaze;
        MoveCell.GetComponent<SpriteRenderer>().enabled = true;
        /*
        for (int f = 0; f < game.large; f++)
        {
            for (int c = 0; c < game.large; c++)
            {
                if (playermaze[f, c] == dado && game.intmaze[f, c] != 60) //no se puede caer en una piedra
                {
                    if (traps.TestingLog(fil, col, f, c, dado) && !antilogs)              //no se puede pasar por despues de un tronco  
                    {
                        continue;
                    }
                    posiblemoves.Add(new int[] { f, c });                                  //casillas azules a las que se puede mover con el valor del dado                     
                    GameObject MoveCellClone = Instantiate(MoveCell, new Vector3((float)(0.5 + c), (float)(0.5 + f), 5), Quaternion.identity);
                    posiblecells.Add(MoveCellClone);
                }
            }
        }*/
        MoveRecursive(fil, col, dado, -1, posiblemoves);
        foreach (int[] move in posiblemoves)
        {
            int f = move[0];
            int c = move[1];
            GameObject MoveCellClone = Instantiate(MoveCell, new Vector3((float)(0.5 + c), (float)(0.5 + f), 5), Quaternion.identity);
            posiblecells.Add(MoveCellClone);
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
        for (int u = 0; u < game.chamanamarcs.Count; u++)
        {
            Destroy(game.chamanamarcs[u]);
        }
        game.chamanamarcs.Clear();
        int[] selcell = new int[] { fcellselected, ccellselected };
        List<int[]> way = new List<int[]> { selcell };
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
                    way.Add(new int[] { newf, newc });           //Creando el camino de atras para alante
                    fcellselected = newf;
                    ccellselected = newc;
                    break;
                }
            }
            num--;

        }
        way.Reverse();                                         //Invirtiendo para conseguir el camino    
        for (int step = 0; step < way.Count; step++)            //Caminando por casillas
        {
            int[] x = way[step];
            int f = x[0];
            int c = x[1];
            fil = f;
            col = c;
            float playerspeed = 5f;
            Vector3 nextPosition = new Vector3(c + 0.5f, f + 0.5f, 1f);

            while (Vector3.Distance(player.transform.position, nextPosition) > 0.01f)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, nextPosition, playerspeed * Time.deltaTime);
                yield return null;
            }
            player.transform.position = nextPosition;
        }
        playerpos = player.transform.position;
        if (game.intmaze[(int)playerpos.y, (int)playerpos.x] != 0 && game.intmaze[(int)playerpos.y, (int)playerpos.x] != 50 && game.intmaze[(int)playerpos.y, (int)playerpos.x] != -5)
        {
            game.newdice = false;
            game.repeatTurn = false;
        }
        traps.Penalizations();
        antilogs = false;
        if ((int)player.transform.position.y == efil && (int)player.transform.position.x == ecol)
        {
            game.gameFinished = true;
            game.InfoText.text = "Encontraste la Gema de la Fortuna";
        }
        game.playerMoved = true;
    }
    void MoveRecursive(int fil, int col, int num,int lastdirection, List<int[]> moves)
    {
        if (num == 0)
        {
            if (game.intmaze[fil, col] == 60) return; // No se puede pasar por una piedra
            if (!IsInList(moves, new int[] { fil, col })) moves.Add(new int[] { fil, col });
            return;
        }
        
        if (game.intmaze[fil, col] == 50 && !antilogs) return; // No se puede pasar por un tronco si no es el último movimiento   
        int[] df = { 1, 0, -1, 0 };
        int[] dc = { 0, 1, 0, -1 };
        for (int dir = 0; dir < 4; dir++)
        {
            if (dir == lastdirection) continue; // Evitar volver en la dirección opuesta
            int newf = fil + df[dir];
            int newc = col + dc[dir];
            if (game.boolmaze[newf, newc]) MoveRecursive(newf, newc, num - 1, (dir + 2) % 4, moves);
        }
    }
    bool IsInList(List<int[]> list, int[] item)
    {
        foreach (int[] element in list)
        {
            if (element[0] == item[0] && element[1] == item[1]) return true;
        }
        return false;
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
