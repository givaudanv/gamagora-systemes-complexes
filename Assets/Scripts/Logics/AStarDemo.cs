using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDemo : MonoBehaviour
{
    GameController gameController;
    GameObject[,] map;
    bool victoire = false;
    bool lance = false;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
        map = gameController.map;
        if (gameObject.GetComponent<MatrixPosition>().objectPosition)
        {
            StartCoroutine(RunAStar());
        }
    }

    void Update()
    {
        if (!lance && gameObject.GetComponent<MatrixPosition>().objectPosition)
        {
            StartCoroutine(RunAStar());
            lance = true;
        }
        /*if (!victoire)
        {
            RunAStar();
        }*/
    }

    IEnumerator RunAStar()
    {
        Dictionary<GameObject, int> openList = new Dictionary<GameObject, int>();
        Dictionary<GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();
        List<GameObject> closedList = new List<GameObject>();
        GameObject playerPos = map[(int)gameController.posPlayer.x, (int)gameController.posPlayer.y];
        GameObject IAPos = gameObject.GetComponent<MatrixPosition>().objectPosition;

        openList.Add(IAPos, 0);

        while (openList.Count > 0 && !victoire)
        {
            GameObject nextNode = MinNode(openList);
            if (nextNode == playerPos)
            {
                Debug.Log("SQUALALA NOUS SOMMES PARTIS !");
                victoire = true;
            }
            else
            {
                openList.Remove(nextNode);
                closedList.Add(nextNode);
                nextNode.GetComponent<Renderer>().material.color = Color.green;
                foreach (GameObject neightbor in nextNode.GetComponent<MapElementController>().neightbors)
                {
                    if (!openList.ContainsKey(neightbor) && !closedList.Contains(neightbor) && neightbor.tag != "Uncrossable")
                    {
                        cameFrom.Add(neightbor, nextNode);
                        openList.Add(neightbor, ComputeCost(neightbor, StepsToReach(neightbor, cameFrom), playerPos));
                        neightbor.GetComponent<Renderer>().material.color = Color.blue;
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
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
        GameObject min = new GameObject();

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
