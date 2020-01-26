using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private BoardManagerV2 boardScript;
    public List<GameObject> gates;

    int turn;
    int round;

    public int turnsPerRound;
    public int roundsPerGame;

    int activePlayer;

    int player1Score;
    int player2Score;
    PlayerImage playerImage;
    TurnsLeftManager turnsLeft;

    public bool gateEntered;

    bool gameDone = false;

    private GameObject settings;

    private SettingsManager settingsScript;

    void Start()
    {
        boardScript = GameObject.Find("BoardManager").GetComponent<BoardManagerV2>();
        gates = new List<GameObject>();
        playerImage = GameObject.Find("PlayerImage").GetComponent<PlayerImage>();
        turnsLeft = GameObject.Find("Turns Left").GetComponent<TurnsLeftManager>();
        InitGame();
    }
    void InitGame()
    {
        settings = GameObject.Find("SettingsManager");
        settingsScript = settings.GetComponent<SettingsManager>();
        DontDestroyOnLoad(settings);
        StartGame();

    }

    void StartGame()
    {
        gateEntered = false;

        turnsPerRound = 3;
        roundsPerGame = 2;

        player1Score = 0;
        player2Score = 0;
        
        turn = 1;
        round = 1;

        activePlayer = 1;
        playerImage.ChangePlayer("Sender");
        turnsLeft.updateTurns(turnsPerRound-turn+1);
    }


    // Update is called once per frame
    void Update()
    {
        if (gateEntered && !gameDone)
        {
            if (turn < turnsPerRound+1)
            {

                if (activePlayer==1)
                {
                    activePlayer = 2;
                    playerImage.ChangePlayer("Blocker");
                }

                else if (activePlayer == 2)
                {
                    activePlayer = 1;
                    playerImage.ChangePlayer("Sender");
                    turn++;
                    turnsLeft.updateTurns(turnsPerRound - turn+1);

                }


                gameObject.GetComponent<MathManager>().CompileForMath();

            }
            if (turn == turnsPerRound+1)
            {
                gameDone = true;
                turnsLeft.GetComponentInChildren<Text>().text="Computing Results!";
                for (int i=0; i<settingsScript.numQubits; i++)
                {
                    //settingsScript.qbitProbs.Add(qbitprob[i]);
                    settingsScript.qbitProbs.Add(0.5f);
                }
                //do math stuff

                // add a wait to build suspense
                StartCoroutine(waitThenEndGame());

                
            }

            gateEntered = false;
        }
    }

    IEnumerator waitThenEndGame()
    {
        float timeToWait = 1.5f;
        float timeWatied = 0f;
        while (timeWatied < timeToWait)
        {
            timeWatied += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("EndMenu");

    }
}
