using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockMaster : MonoBehaviour
{
    [Range(1, 30)]
    public float maxSpeed = 5f;
    [Range(.1f, .5f)]
    public float maxForce = .03f;
    [Range(1, 10)]
    public float neighborhoodRadius = 3f;
    [Range(0, 3)]
    public float separationAmount = 1f;
    [Range(0, 3)]
    public float cohesionAmount = 1f;
    [Range(0, 3)]
    public float alignmentAmount = 1f;
    public bool leaderActive = false;
}
