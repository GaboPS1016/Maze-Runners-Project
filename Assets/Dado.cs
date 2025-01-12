using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{
    public Game game;
    public GameObject dado;
    public Sprite[] diceFaces;
    public bool throwDice = false;
    public void OnMouseDown()                                   //Lanzar el dado al hacer clic sobre el
    {
        if (throwDice) StartCoroutine(dice());       
    }
    IEnumerator dice()
    {
        int value = 1;
        for (int i = 0; i < 10; i++)                                    //Animacion de 10 Valores aleatorios para simular el lanzamiento
        {
            value = Random.Range(0,6);
            dado.GetComponent<Image>().sprite = diceFaces[value];            
            yield return new WaitForSeconds(0.1f);
        }
        game.diceResult = value + 1;
        game.diceThrown = true;
    }
    void Start()
    {
        diceFaces = Resources.LoadAll<Sprite>("DiceFaces/");            //Acceder a las caras del dado de la carpeta
    }
}
