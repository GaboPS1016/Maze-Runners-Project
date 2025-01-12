using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class selectmovecell : MonoBehaviour
{
    public Camera playercamera;
    public Movement movement;
    public List<GameObject> cells;
    public bool select = false;
    public void Selection()
    {
        if (Input.GetMouseButtonDown(0) && select)
        {
            Vector3 mouse = playercamera.ScreenToWorldPoint(Input.mousePosition);
            for (int i = 0; i < cells.Count; i++)
            {
                Collider2D colli = cells[i].GetComponent<Collider2D>();
                if (colli.OverlapPoint(mouse))
                {
                    Debug.Log("tocaste f:" + (int)mouse.y + ", c:" + (int)mouse.x);
                    movement.fcellselected = (int)mouse.y;
                    movement.ccellselected = (int)mouse.x; 
                    movement.cellselected = true;
                    select = false;
                    for (int e = 0; e < cells.Count; e++)
                    {
                        Destroy(cells[e]);
                    }
                    cells.Clear();
                    break;
                }
            }
                           
        }
    }
    void FixedUpdate()
    {
        Selection();
    }
}