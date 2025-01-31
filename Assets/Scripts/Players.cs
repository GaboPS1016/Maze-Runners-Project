using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playerspace
{
    public class Players : ScriptableObject
    {
        public selectmovecell selectmovecell;
        public Game game;
        public Maze_Generator maze;
        public Movement move;
        public GameObject PlayersFolder;
        public int dicevalue {get; set;}
        public bool damaged {get; set;}
        public int burning {get; set;}
        public GameObject player{get; set;}
        public int sleepTime {get; set;}
        public string Ability {get; set;}
        public int rechargeTime {get; set;}
        public int timeToSpecial {get; set;}    
        public Players(int sleepTime = 0, int rechargeTime = 5)
        {
            this.sleepTime = sleepTime;  
            this.rechargeTime = rechargeTime;
            damaged = false;
            burning = 0;
            
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance)
        {
            game = gameInstance;
            selectmovecell = selectInstance;
            maze = mazeInstance;
            move = moveInstance;
        }
        public virtual void special()
        {
            Debug.Log("Special");
        }
    }
    public class Fumador : Players
    {    
        public Fumador() : base(0,2) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Fuma un super cigarro que asquea a los jugadores cercanos, y no pueden usar su habilidad por 3 turnos más.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance,moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            game.fumador.Play();
            int f = (int)player.transform.position.y;
            int c = (int)player.transform.position.x;
            int[,] playermaze = maze.PlayerMaze(f, c);
            List<int> playersAffected = new List<int>();

            for (int i = 0; i < game.numPlayers; i++)
            {
                if (i == game.iactual) continue;
                
                int playerf = (int)game.players[i].transform.position.y;
                int playerc = (int)game.players[i].transform.position.x;
            
                if (playermaze[playerf, playerc] <= 2)
                {
                    playersAffected.Add(i);
                    game.playersInfo[i].timeToSpecial += 3;
                }
            }
            game.InfoText.text = "Fumaste y causaste náuseas.";
        }
    }
    public class Misterioso : Players
    {    
        
        public Misterioso() : base(0,3) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Avanzará la casilla y volverá a jugar, pero avanzará lo mismo.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            game.misterioso.Play();
            dicevalue = game.diceResult;
            game.newdice = true;
            game.InfoText.text = "Vuelves a jugar";
            game.repeatTurn = true;
        }
    }
    public class Bateador : Players
    {    
        public Bateador() : base(0,4) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Lanza bolas y aturde a los jugadores que se encuentren en su fila y columna actual, ";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(2).gameObject;
        }
        public override void special()
        {
            game.bateador.Play();
            for (int i = 0; i < game.numPlayers; i++)
            {
                if (i == game.iactual) continue;
                if ((int)player.transform.position.x == (int)game.players[i].transform.position.x || (int)player.transform.position.y == (int)game.players[i].transform.position.y) game.playersInfo[i].sleepTime += 2;
                game.InfoText.text = "Aturdiste los jugadores en tu columna y en fila";
            }            
        }
    }
    public class Maga : Players
    {    
        public Maga() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Desaparece y aparece en la posición de un jugador aleatorio, su mejor truco de magia.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(3).gameObject;
        }
        public override void special()
        {
            game.maga.Play();
            if (game.numPlayers != 1)
            {
                while(true)
                {
                    for (int i = 0; i < game.numPlayers; i++)
                    {
                        if(i == game.iactual) continue;
                        if(Random.Range(0,100) <= 30)
                        {
                            int f = (int) game.players[i].transform.position.y;
                            int c = (int) game.players[i].transform.position.x;
                            player.transform.position = new Vector3(c + 0.5f, f + 0.5f, 1);
                            game.InfoText.text = "¡Te has teletransportado a otro jugador!";
                            for (int e = 0; e < selectmovecell.cells.Count; e++)
                            {
                                Destroy(selectmovecell.cells[e]);
                            }
                            selectmovecell.cells.Clear();
                            game.playerMoved = true;
                            return;
                        }	
                    }
                }
            }
            else        //numplayers = 1    
            {
                while(true)
                {
                    for (int f = Random.Range(1,game.large - 1); f < game.large - 1; f++)
                    {
                        for (int c = Random.Range(1,game.large - 1); c < game.large - 1; c++)
                        {
                            if(game.intmaze[f,c] == 0 && Random.Range(0,100) <= 30)
                            {
                                player.transform.position = new Vector3(c + 0.5f, f + 0.5f, 1);
                                game.InfoText.text = "¡Te has teletransportado a una casilla segura!";
                                for (int e = 0; e < selectmovecell.cells.Count; e++)
                                {
                                    Destroy(selectmovecell.cells[e]);
                                }
                                selectmovecell.cells.Clear();
                                game.playerMoved = true;
                                return;
                            }                          
                        }
                    } 
                }                 
            }
        }
    }
    public class Mercenario : Players
    {    
        public Mercenario() : base(0,4) 
        {
            timeToSpecial = rechargeTime;
            Ability = "No recibirá la penalización de la trampa en la que caiga.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(4).gameObject;
        }
        public override void special()
        {
            game.mercenario.Play();
            game.inmunity = true;
            game.InfoText.text = "Juega con seguridad, no recibirás efectos de las trampas en este turno.";
            //implementado en traps
        }
    }
    public class Skater : Players
    {    
        public Skater() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Puede moverse hasta 6 casillas en cualquier dirección válida, incluso saltar troncos.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(5).gameObject;

            move.posiblemoves = new List<int[]>();
            move.posiblecells = new List<GameObject>();
        }
        public override void special()
        {
            game.skater.Play();
            game.diceResult = 6; // Cambiamos a 6 para el movimiento
            
            for (int e = 0; e < move.posiblecells.Count; e++)
            {
                Destroy(move.posiblecells[e]);
            }
            move.posiblecells.Clear();
            move.MoveCell.gameObject.SetActive(true);
            move.MoveCell.GetComponent<SpriteRenderer>().enabled = true;
            move.timetomove = true;
        }
    }
    public class Cyborg : Players
    {    
        public Cyborg() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Es capaz de teletransportarse a una posición segura aleatoria, perfecto para salir de atascos.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(6).gameObject;
        }
        public override void special()
        {
            game.cyborg.Play();
            while(true)
            {
                for (int f = Random.Range(1,game.large - 1); f < game.large - 1; f++)
                {
                    for (int c = Random.Range(1,game.large - 1); c < game.large - 1; c++)
                    {
                        if(game.intmaze[f,c] == 0 && Random.Range(0,100) <= 30)
                        {
                            player.transform.position = new Vector3(c + 0.5f, f + 0.5f, 1);
                            game.InfoText.text = "¡Te has teletransportado a una posición segura!";
                            for (int e = 0; e < selectmovecell.cells.Count; e++)
                            {
                                Destroy(selectmovecell.cells[e]);
                            }
                            selectmovecell.cells.Clear();
                            game.playerMoved = true;
                            return;
                        }	
                    }
                }  
            }
        }
    }
    public class Trovador : Players
    {    
        public Trovador() : base(0,4) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Las notas de su guitarra hacen que los jugadores cercanos se duerman por 3 turnos.";
        }
        public void Initialize(Game gameInstance, selectmovecell selectInstance, Maze_Generator mazeInstance, Movement moveInstance, GameObject playersFolder)
        {
            Initialize(gameInstance, selectInstance, mazeInstance, moveInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(7).gameObject;
        }
        public override void special()
        {
            game.trovador.Play();
            int f = (int)player.transform.position.y;
            int c = (int)player.transform.position.x;
            int[,] playermaze = maze.PlayerMaze(f, c);
            List<int> playersAffected = new List<int>();

            for (int i = 0; i < game.numPlayers; i++)
            {
                if (i == game.iactual) continue;
                
                int playerf = (int)game.players[i].transform.position.y;
                int playerc = (int)game.players[i].transform.position.x;
            
                if (playermaze[playerf, playerc] <= 2)
                {
                    playersAffected.Add(i);
                    game.playersInfo[i].sleepTime += 3;
                }
            }
            game.InfoText.text = "Qué musica tan hermosa, dormiste a los oyentes.";
        }
    }
}