using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject prefabCell;
    public int nbCell;

    public float spreadRate;
    public float infectionPower;
    public float infectionSpeed;
    public int nbSpore;
    public float SporeLifetime;
    public float SporeSpeed;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < nbCell; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
            Instantiate(prefabCell, pos, transform.rotation);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("Cell");
            int i = Random.Range(0, go.Length);
            Virus v = new Virus(Color.red, spreadRate, infectionPower, infectionSpeed, nbSpore, SporeLifetime, SporeSpeed);
            go[i].GetComponent<CellController>().Infect(v);
        }
    }
}
