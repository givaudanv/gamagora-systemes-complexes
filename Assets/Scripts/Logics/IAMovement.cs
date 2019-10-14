using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool moving = false;
    GameObject nextPos = null;

    public void Movement(GameObject objectPosition, List<GameObject> path, GameObject target)
    {
        if (moving)
        {
            if (nextPos == objectPosition)
                moving = false;
            else
                MoveTo();
        }

        while (path.Count > 0 && !moving)
        {
            nextPos = path[0];
            path.RemoveAt(0);

            if (nextPos != target)
            {
                MoveTo();
                moving = true;
            }
        }
    }

    void MoveTo()
    {
        float step = speed * Time.deltaTime;
        Vector3 targetPos = new Vector3(nextPos.transform.position.x, transform.position.y, nextPos.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }
}
