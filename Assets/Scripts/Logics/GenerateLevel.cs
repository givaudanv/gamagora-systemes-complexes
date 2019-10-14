using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject gameMaster;
    public Transform map;
    public string mapName = "Assets/Maps/classic.txt";

    GameObject[,] mapElements;
    int nbRow = 0;
    int nbColumn = 0;

    void Awake()
    {
        StreamReader sr = new StreamReader(mapName);

        nbRow = int.Parse(sr.ReadLine());
        nbColumn = int.Parse(sr.ReadLine());

        mapElements = new GameObject[nbRow, nbColumn];

        float initialPosX = 0 - nbColumn;
        float initialPosZ = 0 - nbRow;

        int row = 0;
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            for (int i = 0; i < line.Length; i++)
            {
                GameObject obj = Instantiate(prefabs[(int)char.GetNumericValue(line[i])], new Vector3(initialPosX + (i * 2), 0, initialPosZ + (row * 2)), Quaternion.identity, map);
                mapElements[row, i] = obj;
            }
            row++;
        }

        sr.Close();

        for (int r = 0; r < nbRow; r++)
        {
            for (int c = 0; c < nbColumn; c++)
            {
                setNeightbors(mapElements[r, c], r, c);
            }
        }

        if (gameMaster)
        {
            gameMaster.GetComponent<GameController>().map = mapElements;
            gameMaster.GetComponent<GameController>().nbRow = nbRow;
            gameMaster.GetComponent<GameController>().nbCol = nbColumn;
        }
    }

    void setNeightbors(GameObject obj, int row, int col)
    {
        MapElementController mapElement = obj.GetComponent<MapElementController>();
        mapElement.posMatrix = new Vector2(row, col);
        // if (row < 39 && col > 0)
        //     mapElement.neightbors.Add(mapElements[row + 1, col - 1]);
        if (row < 39)
            mapElement.neightbors.Add(mapElements[row + 1, col]);
        // if (row < 39 && col < 39)
        //     mapElement.neightbors.Add(mapElements[row + 1, col + 1]);
        if (col < 39)
            mapElement.neightbors.Add(mapElements[row, col + 1]);
        // if (row > 0 && col < 39)
        //     mapElement.neightbors.Add(mapElements[row - 1, col + 1]);
        if (row > 0)
            mapElement.neightbors.Add(mapElements[row - 1, col]);
        // if (row > 0 && col > 0)
        //     mapElement.neightbors.Add(mapElements[row - 1, col - 1]);
        if (col > 0)
            mapElement.neightbors.Add(mapElements[row, col - 1]);
    }
}
