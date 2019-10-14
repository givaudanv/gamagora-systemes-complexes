using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    public int taille;

    void Start()
    {
        // Create graph.
        Graph graph = Graph.Create("NewGraph");

        // Create nodes.
        Node nodeA = Node.Create("NodeA");
        Node nodeB = Node.Create("NodeB");
        nodeA.Neighbors.Add(nodeB);

        // Add nodes to graph.
        graph.AddNode(nodeA);
        graph.AddNode(nodeB);
    }
}