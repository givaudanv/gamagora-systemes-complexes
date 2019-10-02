using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionOnStep : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().objectPosition = gameObject;
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().objectPosition = gameObject;
        }
    }
}
