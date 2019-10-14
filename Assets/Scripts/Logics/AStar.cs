using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public GameController gameController;
    bool victoire = false;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
    }

    public List<GameObject> RunAStar(GameObject target = null)
    {
        //gameController.resetColor();
        Dictionary<GameObject, int> openList = new Dictionary<GameObject, int>();
        Dictionary<GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();
        List<GameObject> closedList = new List<GameObject>();
        List<GameObject> path = new List<GameObject>();

        GameObject targetPos = null;
        if (!target)
            targetPos = gameController.player.GetComponent<MatrixPosition>().objectPosition;
        else
            targetPos = target.GetComponent<MatrixPosition>().objectPosition;

        GameObject IAPos = gameObject.GetComponent<MatrixPosition>().objectPosition;
        GameObject nextNode = null;

        openList.Add(IAPos, 0);

        while (openList.Count > 0 && !victoire)
        {
            nextNode = MinNode(openList);
            if (nextNode == targetPos)
            {
                return ReconstructPath(cameFrom, targetPos);
            }
            else
            {
                openList.Remove(nextNode);
                closedList.Add(nextNode);
                //nextNode.GetComponent<Renderer>().material.color = Color.green;
                foreach (GameObject neightbor in nextNode.GetComponent<MapElementController>().neightbors)
                {
                    if (!openList.ContainsKey(neightbor) && !closedList.Contains(neightbor) && neightbor.tag != "Uncrossable")
                    {
                        openList.Add(neightbor, ComputeCost(neightbor, StepsToReach(neightbor, cameFrom), targetPos));
                        cameFrom.Add(neightbor, nextNode);
                        //neightbor.GetComponent<Renderer>().material.color = Color.blue;
                    }
                }
            }
        }

        return ReconstructPath(cameFrom, nextNode);
    }

    int StepsToReach(GameObject target, Dictionary<GameObject, GameObject> cameFrom)
    {
        int steps = 0;
        GameObject current = target;

        while (cameFrom.ContainsKey(current))
        {
            steps++;
            current = cameFrom[current];
        }

        return steps;
    }

    List<GameObject> ReconstructPath(Dictionary<GameObject, GameObject> cameFrom, GameObject lastNode)
    {
        List<GameObject> path = new List<GameObject>();
        path.Add(lastNode);
        GameObject current = lastNode;

        while (cameFrom.ContainsKey(current))
        {
            path.Add(cameFrom[current]);
            current = cameFrom[current];
        }

        path.Reverse();
        return path;
    }

    int ComputeCost(GameObject target, int steps, GameObject player)
    {
        MapElementController targetNode = target.GetComponent<MapElementController>();
        MapElementController playerNode = player.GetComponent<MapElementController>();
        int distanceTargetPlayer = (int)Mathf.Max(Mathf.Abs(targetNode.posMatrix.x - playerNode.posMatrix.x), Mathf.Abs(targetNode.posMatrix.y - playerNode.posMatrix.y));
        int cost = steps + distanceTargetPlayer + targetNode.elementCost;
        return cost;
    }

    GameObject MinNode(Dictionary<GameObject, int> list)
    {
        int minCost = int.MaxValue;
        GameObject min = null;

        foreach (KeyValuePair<GameObject, int> entry in list)
        {
            if (entry.Value < minCost)
            {
                minCost = entry.Value;
                min = entry.Key;
            }
        }

        return min;
    }
}
