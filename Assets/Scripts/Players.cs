using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playerspace
{
    public class Players : ScriptableObject
    {
        public Game game;
        public int sf;
        public int sc;
        public GameObject PlayersFolder;
        public GameObject player{get; set;}
        public int sleepTime {get; set;}
        public int rechargeTime {get; set;}
        public int timeToSpecial {get; set;}    
        public int f {get; set;}
        public int c {get; set;}
        public Players(int sleepTime = 0, int rechargeTime = 5)
        {
            this.sleepTime = sleepTime;  
            this.rechargeTime = rechargeTime;
            
        }
        public void Initialize(Game gameInstance)
        {
            game = gameInstance;
            sf = game.sf;
            sc = game.sc;
            f = sf; 
            c = sc;
        }
        public void special()
        {
            Debug.Log("Special");
        }
    }
    public class Fumador : Players
    {    
        public Fumador() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
           
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
            f = (int)player.transform.position.y;
            c = (int)player.transform.position.x;
        }
        public new void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    

}
