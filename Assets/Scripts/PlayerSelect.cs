using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    //Script para pasar informacion entre escenas
    public static PlayerSelect Instance;
    public int numPlayers;
    public List<int> p;
    public bool multicaminos;
    public int large;
    void Start()                       //crear la referencia de objeto
    {
        Instance = this;
    }
}
