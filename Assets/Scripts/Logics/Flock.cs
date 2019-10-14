using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float maxSpeed;
    public float minDistance;

    List<GameObject> boids;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boids = new List<GameObject>();
        rb.velocity = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        CloseBoids();
        Attract();
        Align();
        Repel();
        ScaleVelocity();
    }

    void ScaleVelocity()
    {
        if (rb.velocity.x > maxSpeed || rb.velocity.y > maxSpeed || rb.velocity.z > maxSpeed)
        {

        }
    }

    void CloseBoids()
    {
        boids.Clear();
        int layer = 1 << LayerMask.NameToLayer("Summons");
        Collider[] summons = Physics.OverlapSphere(transform.position, 20f, layer);
        if (summons.Length > 0)
        {
            foreach (Collider s in summons)
            {
                boids.Add(s.gameObject);
            }
        }
    }

    void Attract()
    {
        Vector3 avgDistance = new Vector3(0, 0, 0);

        foreach (GameObject b in boids)
        {
            avgDistance += transform.position - b.transform.position;
        }

        avgDistance /= boids.Count;
        rb.velocity -= avgDistance / 100;
    }

    void Align()
    {
        Vector3 avgVelocity = new Vector3(0, 0, 0);

        foreach (GameObject b in boids)
        {
            avgVelocity += b.GetComponent<Rigidbody>().velocity;
        }

        avgVelocity /= boids.Count;
        rb.velocity += avgVelocity / 40;
    }

    void Repel()
    {
        Vector3 distanceDiff = new Vector3(0, 0, 0);
        int numClose = 0;

        foreach (GameObject b in boids)
        {
            float distance = Vector3.Distance(transform.position, b.transform.position);
            if (distance < minDistance)
            {
                numClose += 1;
                Vector3 diff = (transform.position - b.transform.position);
                if (diff.x >= 0)
                    diff.x = Mathf.Sqrt(minDistance) - diff.x;
                else
                    diff.x = -Mathf.Sqrt(minDistance) - diff.x;
                if (diff.y >= 0)
                    diff.y = Mathf.Sqrt(minDistance) - diff.y;
                else
                    diff.y = -Mathf.Sqrt(minDistance) - diff.y;
                if (diff.z >= 0)
                    diff.z = Mathf.Sqrt(minDistance) - diff.z;
                else
                    diff.z = -Mathf.Sqrt(minDistance) - diff.z;
                distanceDiff += diff;
            }
        }

        if (numClose > 0)
        {
            rb.velocity -= distanceDiff / 5;
        }
    }
}
