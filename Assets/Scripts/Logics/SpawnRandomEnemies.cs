using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomEnemies : MonoBehaviour
{
    public GameObject goblin;

    void Start()
    {
        InvokeRepeating("Spawn", 0f, 5f);
    }

    void Spawn()
    {
        Instantiate(goblin, new Vector3(Random.Range(-20, 20), 1, Random.Range(-20, 20)), transform.rotation);
    }
}
