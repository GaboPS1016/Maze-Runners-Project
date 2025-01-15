using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pdescription : MonoBehaviour
{    
    public List<string> pStrings;
    void Start()
    {
        string s0 = "Fumador        ";
        string s1 = "Misterioso     ";
        string s2 = "Bateador       ";
        string s3 = "Estilista      ";
        string s4 = "Mercenario     ";
        string s5 = "Skater         ";
        string s6 = "Cyborg         ";
        string s7 = "Trovador       ";

        pStrings = new List<string> {s0, s1, s2, s3, s4, s5, s6, s7};
    }
}
