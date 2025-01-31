using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pdescription : MonoBehaviour
{    
    public List<string> pStrings;
    void Start()
    {
        string s0 = "Fumador                Su boca es incapaz de estar segundos sin fumar, por lo que su olor repugna a los demás";
        string s1 = "Misterioso             No se sabe nada sobre él, seguro planea algo, causa escalofríos.";
        string s2 = "Bateador               Se dice que nunca ha perdido en su deporte, su fuerte son los Home Run";
        string s3 = "Maga                   Tiene gran talento para las artes místicas, sin embargo le queda mucho por aprender.";
        string s4 = "Mercenario             Audaz y fornido, su gran físico hace que hasta las trampas le teman.";
        string s5 = "Skater                 El más veloz si de ir sobre ruedas se trata, capaz de saltar obstáculos con su patineta.";
        string s6 = "Cyborg                 Posee la tecnología para viajar por el espacio tiempo, pero no la logra controlar del todo.";
        string s7 = "Trovador               Gran poeta melancólico, las notas de su guitarra alivian los pesares de los oyentes.";

        pStrings = new List<string> {s0, s1, s2, s3, s4, s5, s6, s7};
    }
}
