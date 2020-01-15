using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private BoardManager boardScript;
    public List<GameObject> gates;
    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        gates = new List<GameObject>();
        InitGame();
    }
    void InitGame()
    {
        boardScript.SetupScene();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
