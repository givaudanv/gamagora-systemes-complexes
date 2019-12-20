using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeController : MonoBehaviour
{
    public Virus virus;
    public GameObject cell;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().velocity = transform.up * virus.sporeSpeed;
        GetComponent<MeshRenderer>().material.color = virus.color;
        Destroy(gameObject, virus.sporeLifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell" && other.gameObject != cell)
        {
            if (other.GetComponent<CellController>().virus != virus) other.GetComponent<CellController>().Infect(virus);
            Destroy(gameObject);
        }
    }
}
