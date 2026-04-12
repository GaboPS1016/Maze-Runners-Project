using UnityEngine;
using System.IO;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Guardar : MonoBehaviour
{
    public Game game;
    public GameObject panelguardado;
    public GameObject panelconfirmacion;
    public string archivodeguardado;
    public TextMeshProUGUI inputnombre;
    public string nombrepartida;
    public int ranura;
    public TextMeshProUGUI r1;
    public TextMeshProUGUI r2;
    public TextMeshProUGUI r3;
    public void PanelGuardado()
    {
        
        string ruta1 = Application.dataPath + "/Ranura1.json";
        string ruta2 = Application.dataPath + "/Ranura2.json";
        string ruta3 = Application.dataPath + "/Ranura3.json";
        if (File.Exists(ruta1)) r1.text = JsonUtility.FromJson<DatosJuego>(File.ReadAllText(ruta1)).nombrepartida;
        else r1.text = "vacío";
        if (File.Exists(ruta2)) r2.text = JsonUtility.FromJson<DatosJuego>(File.ReadAllText(ruta2)).nombrepartida;
        else r2.text = "vacío";
        if (File.Exists(ruta3)) r3.text = JsonUtility.FromJson<DatosJuego>(File.ReadAllText(ruta3)).nombrepartida;
        else r3.text = "vacío";
        panelguardado.SetActive(true);
    }
    public void CerrarPanelGuardado()
    {
        panelguardado.SetActive(false);
    }
    public void AtrasPanelConfirmacion()
    {
        inputnombre.text = "";
        panelconfirmacion.SetActive(false);
    }
    public void Ranuras(int num)
    {
        ranura = num;
        panelconfirmacion.SetActive(true);
    }
    public void AceptarPanelConfirmacion()
    {
        nombrepartida = inputnombre.text;
        if (inputnombre.text.Length == 0) nombrepartida = "Ranura " + Convert.ToString(ranura);
        inputnombre.text = "";
        panelconfirmacion.SetActive(false);
        panelguardado.SetActive(false);
        Guardarr(ranura, nombrepartida);
    }
    public void Guardarr(int ranurap, string nombrepartidap)
    {

        DatosJuego nuevosDatos = new DatosJuego()
        {
            nombrepartida = nombrepartidap,
            ranura = ranurap,
            intmaze = AplanarMatrizEntera(game.intmaze),
            boolmaze = AplanarMatrizBooleana(game.boolmaze),
            numplayers = game.numPlayers,
            p = game.p,
            positions = PlayersPositions(),
            timetospecial = Properties(0),
            sleeptime = Properties(1),
            burning = Properties(2),
            damageds = Damageds(),
            iactual = game.iactual,
            start = new int[2] { game.sf, game.sc },
            gema = new int[2] { game.ff, game.fc },
        };
        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
        archivodeguardado = Application.dataPath + "/Ranura" + ranurap + ".json";
        File.WriteAllText(archivodeguardado, cadenaJSON);
    }
    public List<Vector3> PlayersPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < game.numPlayers; i++)
        {
            positions.Add(game.players[i].transform.position);
        }
        return positions;
    }
    public List<int> Properties(int index)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < game.numPlayers; i++)
        {
            switch (index)
            {
                case 0:
                    list.Add(game.playersInfo[i].timeToSpecial);
                    break;
                case 1:
                    list.Add(game.playersInfo[i].sleepTime);
                    break;
                default:
                    list.Add(game.playersInfo[i].burning);
                    break;
            }
        }
        return list;
    }
    public List<bool> Damageds()
    {
        List<bool> dam = new List<bool>();
        for (int i = 0; i < game.numPlayers; i++)
        {
            dam.Add(game.playersInfo[i].damaged);
        }
        return dam;
    }

    public int[] AplanarMatrizEntera(int[,] matriz)
    {
        int n = matriz.GetLength(0);
        int m = matriz.GetLength(1);
        int[] array = new int[n * m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                array[i * m + j] = matriz[i, j];
        return array;
    }
    public bool[] AplanarMatrizBooleana(bool[,] matriz)
    {
        int n = matriz.GetLength(0);
        int m = matriz.GetLength(1);
        bool[] array = new bool[n * m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                array[i * m + j] = matriz[i, j];
        return array;
    }
}
