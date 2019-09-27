using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionOnStep : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().objectPosition = gameObject;
        }
    }
}
