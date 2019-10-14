using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1;

    List<GameObject> path = new List<GameObject>();
    GameObject moveTarget = null;
    MatrixPosition matrixPos;
    AStar aStar;

    void Start()
    {
        matrixPos = GetComponent<MatrixPosition>();
        aStar = GetComponent<AStar>();
        InvokeRepeating("RunAStar", 0f, 1f);
    }

    void Update()
    {
        GetComponent<IAMovement>().Movement(matrixPos.objectPosition, path, moveTarget);
    }

    void RunAStar()
    {
        if (matrixPos.objectPosition != null)
        {
            path = aStar.RunAStar();
            moveTarget = path[path.Count - 1];
        }
    }
}
