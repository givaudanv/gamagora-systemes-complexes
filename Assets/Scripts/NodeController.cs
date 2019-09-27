using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public bool depart = false;
    public bool arrivee = false;
    public int valeur;
    public Voisin[] voisins;

    void Start()
    {
        if (depart)
        {
            valeur = 0;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            valeur = int.MaxValue;
        }

        if (arrivee)
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
