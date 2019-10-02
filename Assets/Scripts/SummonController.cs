using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonController : MonoBehaviour
{
    public float power;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }
}
