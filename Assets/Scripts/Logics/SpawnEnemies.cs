using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject weakling;
    public GameObject flock;

    public int max;

    void Start()
    {
        InvokeRepeating("Spawn", 0f, 8f);
        //for (int i = 0; i <= max; i++)
        //   Instantiate(flock, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
        //Spawn();
    }

    void Spawn()
    {
        Instantiate(weakling, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
    }
}
