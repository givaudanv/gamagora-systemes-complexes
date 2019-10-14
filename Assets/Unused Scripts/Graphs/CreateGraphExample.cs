using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CreateGraphExample
{
    [MenuItem("Window/Graph Serialization Example/Create Graph")]
    public static void CreateGraph()
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
