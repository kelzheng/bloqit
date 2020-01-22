using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManagerV2 : MonoBehaviour
{
    SettingsManager settingsManager;
    // Start is called before the first frame update
    public int columns = 8;
    public int rows = 2;

    public GameObject line;
    public GameObject[] qubitStart;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void Start()
    {
        settingsManager = GameObject.Find("SettingsManager").GetComponent<SettingsManager>();
        rows = settingsManager.numQubits;
        listInit();
        boardSetup();
    }
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
        for (int x = -1; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if(x == -1)
                {
                    GameObject instance = Instantiate(qubitStart[y], new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
                else
                {
                    GameObject instance = Instantiate(line, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }

    }
}
