using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playerspace
{
    public class Players : ScriptableObject
    {
        public Game game;
        public GameObject PlayersFolder;
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
        public void Initialize(Game gameInstance)
        {
            game = gameInstance;
        }
        public virtual void special()
        {
            Debug.Log("Special");
        }
    }
    public class Fumador : Players
    {    
        public Fumador() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "Fumar";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    public class Misterioso : Players
    {    
        public Misterioso() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "No se sabe";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("?????????");
        }
    }
    public class Bateador : Players
    {    
        public Bateador() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    public class Maga : Players
    {    
        public Maga() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    public class Mercenario : Players
    {    
        public Mercenario() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    public class Skater : Players
    {    
        public Skater() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    public class Cyborg : Players
    {    
        public Cyborg() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
    public class Trovador : Players
    {    
        public Trovador() : base(0,5) 
        {
            timeToSpecial = rechargeTime;
            Ability = "";
        }
        public void Initialize(Game gameInstance, GameObject playersFolder)
        {
            Initialize(gameInstance);
            PlayersFolder = playersFolder;
            player = PlayersFolder.transform.GetChild(0).gameObject;
        }
        public override void special()
        {
            Debug.Log("Smooooooooooke");
        }
    }
}