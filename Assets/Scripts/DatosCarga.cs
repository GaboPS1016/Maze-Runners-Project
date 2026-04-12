using UnityEngine;
using System.Collections.Generic;
using Playerspace;
public class DatosCarga : MonoBehaviour
{
    public static DatosCarga Instance;
    public string nombrepartida;
    public int ranura;
    public int[,] intmaze;
    public bool[,] boolmaze;
    public int numplayers;
    public List<int> p;
    public List<Vector3> positions;
    public List<int> timetospecial;
    public List<int> sleeptime;
    public List<int> burning;
    public List<bool> damageds;
    public int iactual;
    public int[] start;
    public int[] gema;
    void Start()
    {
        Instance = this;
    }

}
