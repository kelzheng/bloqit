using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private BoardManager boardScript;
    public List<GameObject> gates;

    int turn;
    int round;

    public int turnsPerRound;
    public int roundsPerGame;

    int activePlayer;

    int player1Score;
    int player2Score;

    public bool gateEntered;

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        gates = new List<GameObject>();
        InitGame();
    }
    void InitGame()
    {
        boardScript.SetupScene();
        StartGame();

    }

    void StartGame()
    {
        gateEntered = false;

        turnsPerRound = 8;
        roundsPerGame = 2;

        player1Score = 0;
        player2Score = 0;
        
        turn = 1;
        round = 1;

        activePlayer = 1;
        Debug.Log("0");
        Debug.Log("Player 1's turn");
        Debug.Log("Player 1, please choose a place to place your gate");


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gateEntered)
        {
            if (turn < turnsPerRound)
            {
                turn++;

                if (activePlayer==1)
                {
                    activePlayer = 2;
                }

                else if (activePlayer == 2)
                {
                    activePlayer = 1;
                }

                Debug.Log("1");
                Debug.Log("Player " + activePlayer + "'s turn");
                Debug.Log("Player " +activePlayer +", please choose a place to place your gate");

            }
            else if (turn == turnsPerRound)
            {
                turn = 1;
                round++;

                if (activePlayer == 1)
                {
                    activePlayer = 2;
                }

                if (activePlayer == 2)
                {
                    activePlayer = 1;
                }
                Debug.Log("2");
                Debug.Log("Player " + activePlayer + "'s turn");
                Debug.Log("Player " + activePlayer + ", please choose a place to place your gate");
            }

            gateEntered = false;
        }
    }
}
