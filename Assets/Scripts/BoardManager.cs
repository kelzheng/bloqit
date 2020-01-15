    using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int columns = 8;
    public int rows = 3;
    public GameObject[] floorTiles;
    public GameObject wallTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void listInit()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void boardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate;

                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
                else
                {
                    toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);

                } 
                
                // GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                // if (x == -1 || x == columns || y == -1 || y == rows)
                //     toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                // GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                // instance.transform.SetParent(boardHolder);

            }
        }

    }


    public void SetupScene()
    {
        boardSetup();
        listInit();
    }
}
