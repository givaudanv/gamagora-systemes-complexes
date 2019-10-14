using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    public GameObject depart;
    public GameObject arrivee;
    public List<GameObject> nodes;

    List<GameObject> sousGraphe;
    Dictionary<GameObject, GameObject> pred;
    int curseur = 0;

    void Start()
    {
        sousGraphe = new List<GameObject>();
        pred = new Dictionary<GameObject, GameObject>();
        sousGraphe.Add(depart);
        nodes.Remove(depart);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            step();
    }

    void step()
    {
        NodeController villeCourante = sousGraphe[curseur].GetComponent<NodeController>();
        Voisin[] voisinsCourant = villeCourante.voisins;
        for (int i = 0; i < voisinsCourant.Length; i++)
        {
            int nouvelleValeur = villeCourante.valeur + voisinsCourant[i].distance;
            if (nouvelleValeur < voisinsCourant[i].voisin.GetComponent<NodeController>().valeur)
            {
                voisinsCourant[i].voisin.GetComponent<NodeController>().valeur = nouvelleValeur;
                pred.Remove(voisinsCourant[i].voisin);
                pred.Add(voisinsCourant[i].voisin, villeCourante.gameObject);
            }
        }
        villeCourante.GetComponent<Renderer>().material.color = Color.grey;
        GameObject min = minimalHorsSousGraphe();
        sousGraphe.Add(min);
        if (min == arrivee)
        {
            victoire();
        }
        else
        {
            nodes.Remove(min);
            curseur++;
        }
    }

    GameObject minimalHorsSousGraphe()
    {
        GameObject min = nodes[0];
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].GetComponent<NodeController>().valeur < min.GetComponent<NodeController>().valeur)
                min = nodes[i];
        }
        return min;
    }

    void victoire()
    {
        Debug.Log("Chemin le plus court :");
        GameObject hansel = arrivee;
        while (hansel != depart)
        {
            Debug.Log(hansel);
            hansel = pred[hansel];
        }
        Debug.Log(hansel);
    }
}

//lance un rayon vers la lampe
//si intersect avant lampe = ombre
//sinon éclairé
