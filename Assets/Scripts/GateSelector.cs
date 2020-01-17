using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSelector : MonoBehaviour
{

    [SerializeField]
    GameObject gate;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        GameObject instance = Instantiate(gate, transform.position, Quaternion.identity);

        gameManager.gates.Add(instance);

    }

}
