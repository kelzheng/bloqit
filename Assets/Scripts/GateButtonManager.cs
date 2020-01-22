using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateButtonManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] gates;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
    }

    public void CreateGate(int gate)
    {
        GameObject instance = Instantiate(gates[gate], transform.position, Quaternion.identity);

        gameManager.gates.Add(instance);

    }
}
