using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject objectPosition;
    public float speed = 1;

    public List<GameObject> path = new List<GameObject>();
    GameObject target = null;
    GameObject nextPos = null;
    public bool moving = false;
    AStar aStar;

    void Start()
    {
        aStar = GetComponent<AStar>();
        InvokeRepeating("RunAStar", 0f, 1f);
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (moving)
        {
            if (nextPos == objectPosition)
                moving = false;
            else
                MoveTo(nextPos);
        }

        while (path.Count > 0 && !moving)
        {
            nextPos = path[0];
            path.RemoveAt(0);

            if (nextPos != target)
            {
                MoveTo(nextPos);
                moving = true;
            }
        }
    }

    void MoveTo(GameObject target)
    {
        float step = speed * Time.deltaTime;
        Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    void RunAStar()
    {
        path = aStar.RunAStar();
        target = path[path.Count - 1];
        moving = false;
        nextPos = null;
    }
}
