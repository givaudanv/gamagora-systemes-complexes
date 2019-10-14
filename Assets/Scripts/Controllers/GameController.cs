using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[,] map;
    public GameObject player;
    public int nbRow;
    public int nbCol;
    public Vector2 posPlayer = new Vector2(7, 7);

    public void resetColor()
    {
        for (int r = 0; r < nbRow; r++)
        {
            for (int c = 0; c < nbCol; c++)
            {
                map[r, c].GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}