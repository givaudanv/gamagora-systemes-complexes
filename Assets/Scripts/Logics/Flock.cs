using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockMaster flockM;
    public GameObject leader;
    public Vector3 velocity;

    private bool leaderActive;
    private float maxSpeed;
    private float maxForce;
    private float neighborhoodRadius;
    private float separationAmount;
    private float cohesionAmount;
    private float alignmentAmount;

    private Vector3 acceleration;
    private List<GameObject> boids;

    void Start()
    {
        leader = GameObject.FindGameObjectWithTag("FlockM");
        flockM = leader.GetComponent<FlockMaster>();
        boids = new List<GameObject>();
    }

    void FixedUpdate()
    {
        maxSpeed = flockM.maxSpeed;
        maxForce = flockM.maxForce;
        neighborhoodRadius = flockM.neighborhoodRadius;
        separationAmount = flockM.separationAmount;
        cohesionAmount = flockM.cohesionAmount;
        alignmentAmount = flockM.alignmentAmount;
        leaderActive = flockM.leaderActive;
        CloseBoids();
        Flocking();
        UpdateVelocity();
        UpdatePosition();
    }

    public void UpdateVelocity()
    {
        velocity += acceleration;
        velocity = LimitMagnitude(velocity, maxSpeed);
    }

    private void UpdatePosition()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void Flocking()
    {
        var alignment = Align();
        var repel = Repel();
        var attract = Attract();

        acceleration = alignmentAmount * alignment + cohesionAmount * attract + separationAmount * repel;
    }

    void CloseBoids()
    {
        int layer = 1 << LayerMask.NameToLayer("Summons");
        var boidColliders = Physics.OverlapSphere(transform.position, neighborhoodRadius, layer);
        boids = boidColliders.Select(o => o.gameObject).ToList();
        boids.Remove(gameObject);
    }

    Vector3 Attract()
    {
        if (boids.Count == 0 && !leaderActive) return Vector3.zero;

        Vector3 sumPos = Vector3.zero;

        foreach (GameObject b in boids)
        {
            sumPos += b.transform.position;
        }

        if (leaderActive)
        {
            sumPos += leader.transform.position;
            sumPos /= boids.Count + 1;
        }
        else
        {
            sumPos /= boids.Count;
        }


        Vector3 direction = sumPos - transform.position;
        return Steer(direction.normalized * maxSpeed);
    }

    Vector3 Align()
    {
        Vector3 avgVelocity = Vector3.zero;
        if (boids.Count == 0) return avgVelocity;

        foreach (GameObject b in boids)
        {
            avgVelocity += b.GetComponent<Flock>().velocity;
        }

        avgVelocity /= boids.Count;
        return Steer(avgVelocity.normalized * maxSpeed);
    }

    Vector3 Repel()
    {
        Vector3 direction = Vector3.zero;
        if (boids.Count == 0) return direction;

        int numClose = 0;

        foreach (GameObject b in boids)
        {
            float distance = Vector3.Distance(transform.position, b.transform.position);
            if (distance < neighborhoodRadius / 2)
            {
                numClose += 1;
                Vector3 difference = (transform.position - b.transform.position);
                direction += difference.normalized / difference.magnitude;
            }
        }

        direction /= numClose;
        return Steer(direction.normalized * maxSpeed);
    }

    private Vector3 Steer(Vector3 desired)
    {
        var steer = desired - velocity;
        steer = LimitMagnitude(steer, maxForce);

        return steer;
    }

    private Vector3 LimitMagnitude(Vector3 baseVector, float maxMagnitude)
    {
        if (baseVector.sqrMagnitude > maxMagnitude * maxMagnitude)
        {
            baseVector = baseVector.normalized * maxMagnitude;
        }
        return baseVector;
    }
}
