using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pdescription : MonoBehaviour
{
    public List<string> pStrings;
    public List<string> infStrings;

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
        string s8 = "Asesino                No está muy bien de la cabeza, disfruta ver sufrir a los demás.";
        string s9 = "Chamana                Es capaz de orientarse con su magia de la naturaleza, era líder de una tribu.";
        pStrings = new List<string> { s0, s1, s2, s3, s4, s5, s6, s7, s8, s9 };
        
        string i0 = "TR 2. Hace que los jugadores a 5 casillas no pueden usar su habilidad por 3 turnos más. Juega doble en los venenos";
        string i1 = "TR 3. Avanzará la casilla y volverá a jugar, avanzando lo mismo de nuevo.";
        string i2 = "TR 5. Aturde por 3 turnos a los jugadores que se encuentren en su fila o columna actual.";
        string i3 = "TR 7. Desaparece y aparece en la posición de un jugador aleatorio.";
        string i4 = "TR 3. No recibirá la penalización si cae en una trampa en ese turno.";
        string i5 = "TR 5. Puede moverse 6 casillas en cualquier dirección válida, incluso saltar troncos.";
        string i6 = "TR 6. Se teletransporta a una posición segura aleatoria.";
        string i7 = "TR 4. Hace que los jugadores a 5 casillas se duerman por 4 turnos.";
        string i8 = "TR 8. Manda a los jugadores a 1 casilla, al inicio.";
        string i9 = "TR 2. Te indica el camino más rápido a la gema.";
        infStrings = new List<string> { i0, i1, i2, i3, i4, i5, i6, i7, i8, i9 };
    }
}
