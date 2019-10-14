using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBoundaries : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        other.transform.position = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), Random.Range(0 - 25, 25));
    }
}
