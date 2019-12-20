using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    public GameObject prefabVessel;
    public bool infected;
    public bool infecting;
    public Virus virus;

    private float spreadTimer;

    void Start()
    {
        spreadTimer = 0f;
    }

    void Update()
    {
        if (infected)
        {
            if (spreadTimer >= virus.spreadRate)
            {
                Spread();
                spreadTimer = 0f;
            }
            else
            {
                spreadTimer += Time.deltaTime;
            }
        }
    }

    private void Spread()
    {
        List<GameObject> gos = new List<GameObject>();

        for (int i = 0; i < virus.nbSpore; i++)
        {
            gos.Add(Instantiate(prefabVessel, transform.position, Quaternion.Euler(new Vector3(Random.Range(0f, 360f), 0, Random.Range(0f, 360f)))));
        }

        foreach (GameObject go in gos)
        {
            go.GetComponent<SporeController>().virus = virus;
            go.GetComponent<SporeController>().cell = gameObject;
        }
    }

    public void Infect(Virus infectingVirus)
    {
        virus = infectingVirus;
        infected = true;
        gameObject.GetComponent<MeshRenderer>().material.color = infectingVirus.color;
    }
}
