using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixPosition : MonoBehaviour
{
    public GameObject objectPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
            objectPosition = other.gameObject;
    }
}
