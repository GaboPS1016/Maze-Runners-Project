using UnityEngine;
using System.IO;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Playerspace;
using System.Collections.Generic;

public class Cargar : MonoBehaviour
{
    public DatosCarga d;
    public GameObject panelcargado;
    public TextMeshProUGUI r1;
    public TextMeshProUGUI r2;
    public TextMeshProUGUI r3;
    public GameObject elim1;
    public GameObject elim2;
    public GameObject elim3;
    public DatosJuego datosjuego = new DatosJuego();
    public void PanelCargado()
    {
        string ruta1 = Application.dataPath + "/Ranura1.json";
        string ruta2 = Application.dataPath + "/Ranura2.json";
        string ruta3 = Application.dataPath + "/Ranura3.json";
        if (File.Exists(ruta1))
        {
            r1.text = JsonUtility.FromJson<DatosJuego>(File.ReadAllText(ruta1)).nombrepartida;
            elim1.SetActive(true);
        }
        else
        {
            r1.text = "vacío";
            elim1.SetActive(false);
        }
        if (File.Exists(ruta2))
        {
            r2.text = JsonUtility.FromJson<DatosJuego>(File.ReadAllText(ruta2)).nombrepartida;
            elim2.SetActive(true);
        }
        else
        {
            r2.text = "vacío";
            elim2.SetActive(false);
        }
        if (File.Exists(ruta3))
        {
            r3.text = JsonUtility.FromJson<DatosJuego>(File.ReadAllText(ruta3)).nombrepartida;
            elim3.SetActive(true);
        }
        else
        {
            r3.text = "vacío";
            elim3.SetActive(false);
        }
        panelcargado.SetActive(true);
    }
    public void CerrarPanelCargado()
    {
        panelcargado.SetActive(false);
    }
    public void Cargarr(int num)
    {
        GameObject pHolder = GameObject.Find("PlayerSelect");
        if (pHolder != null) Destroy(pHolder);
        GameObject[] elims = { elim1, elim2, elim3 };
        if (!elims[num - 1].gameObject.activeSelf) return;
        string ruta = Application.dataPath + "/Ranura" + num + ".json";
        string contenido = File.ReadAllText(ruta);
        datosjuego = JsonUtility.FromJson<DatosJuego>(contenido);
        GameObject datos = GameObject.Find("datos");
        
        d.nombrepartida = datosjuego.nombrepartida;
        d.ranura = datosjuego.ranura;
        d.intmaze = FormarMatrizEntera(datosjuego.intmaze);
        d.boolmaze = FormarMatrizBooleana(datosjuego.boolmaze);
        d.numplayers = datosjuego.numplayers;
        d.p = datosjuego.p;
        d.positions = datosjuego.positions;
        d.timetospecial = datosjuego.timetospecial;
        d.sleeptime = datosjuego.sleeptime;
        d.burning = datosjuego.burning;
        d.damageds = datosjuego.damageds;
        d.iactual = datosjuego.iactual;
        d.start = datosjuego.start;
        d.gema = datosjuego.gema;
        DontDestroyOnLoad(datos);
        SceneManager.LoadScene(3);
    }
    public void EliminarPartida(int num)
    {
        string ruta = Application.dataPath + "/Ranura" + num + ".json";
        File.Delete(ruta);
        if (File.Exists(ruta + ".meta")) File.Delete(ruta + ".meta");
        GameObject[] elims = { elim1, elim2, elim3 };
        TextMeshProUGUI[] r = { r1, r2, r3 };
        elims[num - 1].SetActive(false);
        r[num - 1].text = "vacío";

    }
    public int[,] FormarMatrizEntera(int[] array)
    {
        int n = (int)Math.Sqrt(array.Length);
        int[,] matriz = new int[n,n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matriz[i, j] = array[i * n + j];
        return matriz;
    }
    public bool[,] FormarMatrizBooleana(bool[] array)
    {
        int n = (int)Math.Sqrt(array.Length);
        bool[,] matriz = new bool[n,n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matriz[i, j] = array[i * n + j];
        return matriz;
    }
}
