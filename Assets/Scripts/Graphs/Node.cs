using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : ScriptableObject
{
    [SerializeField]
    private List<Node> neighbors;
    public List<Node> Neighbors
    {
        get
        {
            if (neighbors == null)
            {
                neighbors = new List<Node>();
            }

            return neighbors;
        }
    }

    public static Node Create(string name)
    {
        Node node = CreateInstance<Node>();
        node.name = name;
        return node;
    }
}
