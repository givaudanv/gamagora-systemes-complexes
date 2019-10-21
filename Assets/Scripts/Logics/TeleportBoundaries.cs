using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBoundaries : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        other.transform.position = transform.position - other.transform.position;
    }
}
