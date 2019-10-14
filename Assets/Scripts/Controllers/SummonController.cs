using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    public float range = 5f;

    List<GameObject> path = new List<GameObject>();
    public GameObject moveTarget = null;
    public GameObject target = null;
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
        if (!target)
        {
            int layer = 1 << LayerMask.NameToLayer("Enemies");
            Collider[] enemies = Physics.OverlapSphere(transform.position, 20f, layer);
            if (enemies.Length > 0)
                target = enemies[Random.Range(0, enemies.Length - 1)].gameObject;
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range)
            {
                Destroy(target);
                GetComponent<IAMovement>().moving = false;
            }
            else
            {
                GetComponent<IAMovement>().Movement(matrixPos.objectPosition, path, moveTarget);
            }
        }
    }

    void RunAStar()
    {
        if (matrixPos.objectPosition != null && target)
        {
            path = aStar.RunAStar(target);
            moveTarget = path[path.Count - 1];
        }
    }
}
