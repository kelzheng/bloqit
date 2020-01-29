using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: make actual collision and not a janky versio

public class GateDrag : MonoBehaviour
{
    bool holding = true;
    bool canDrop = true;
    bool inCircuit = false;
    BoardManagerV2 board;
    GameManager gameManager;
    Vector3 point;

    AudioManager plop;

    //   Collider2D[] results;
    //   GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("BoardManager").GetComponent<BoardManagerV2>();
        //results = new Collider2D[board.columns * board.rows];
        gameManager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        plop = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame


    /*void OnTriggerStay2D(Collision2D col)
    {
        Debug.Log(col);
    }*/
    void Update()
    {
        canDrop = true;
        inCircuit = false;
        if (holding)
        {
            //keyboard controls
        #if UNITY_STANDALONE || UNITY_WEBPLAYER
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((point.x >=-0.5 && point.x <= board.columns+0.45-1) && (point.y >= -0.5 && point.y <= board.rows+0.45-1))
            {
                inCircuit = true;
                point.x = Mathf.RoundToInt(point.x);
                point.y = Mathf.RoundToInt(point.y);

                //changes the center point if the gate is longer than 1x1
                if (gameObject.GetComponent<GateManager>().numQubits > 1)
                {
                    point.y -= (float)0.5*(gameObject.GetComponent<GateManager>().numQubits-1);
                }
                
                //if the target if off the edge
                if (gameObject.GetComponent<GateManager>().numQubits > 1){

                    if ((point.y + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) - 1 < 0)
                    {
                        canDrop = false;
                    }
                 }
                
                foreach(GameObject gate in gameManager.gates)
                {
                    if(gate != gameObject)
                    {
                        //if the gate to drop is 2 qbit
                        if (gameObject.GetComponent<GateManager>().numQubits > 1)
                        {
                            //this gate hits another gate
                            if (point.x == gate.GetComponent<GateManager>().moment && 
                                (point.y + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().qbit)
                            {
                                canDrop = false;
                            }
                            //this gate hits a target
                            if (point.x == gate.GetComponent<GateManager>().moment &&
                                (point.y + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().target)
                            {
                                canDrop = false;
                            }
                            //this target hits a gate
                            if (point.x == gate.GetComponent<GateManager>().moment &&
                                (point.y - 1 + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().qbit)
                            {
                                canDrop = false;
                            }
                            //this target hits a gate
                            if (point.x == gate.GetComponent<GateManager>().moment &&
                                (point.y - 1 + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().target)
                            {
                                canDrop = false;
                            }

                        }

                        //if the gate to drop hits another gate
                        if (point.x == gate.GetComponent<GateManager>().moment && point.y == gate.GetComponent<GateManager>().qbit)
                        {
                            canDrop = false;
                        }
                        //checks for 2 qbit spots
                        if (gate.GetComponent<GateManager>().numQubits>1)
                        {
                            if (point.x == gate.GetComponent<GateManager>().moment && point.y == gate.GetComponent<GateManager>().target)
                            {
                                canDrop = false;
                            }
                        }
                    }
                }
            }
            //Mobile controls
            #else
            if (Input.touchCount > 0)
            {
                Touch myTouch = Input.touches[0];

                point = Camera.main.ScreenToWorldPoint(myTouch.position);
                if ((point.x >= -0.5 && point.x <= board.columns + 0.45 - 1) && (point.y >= -0.5 && point.y <= board.rows + 0.45 - 1))
                {
                    inCircuit = true;
                    point.x = Mathf.RoundToInt(point.x);
                    point.y = Mathf.RoundToInt(point.y);

                    //changes the center point if the gate is longer than 1x1
                    if (gameObject.GetComponent<GateManager>().numQubits > 1)
                    {
                        point.y -= (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1);
                    }

                    foreach (GameObject gate in gameManager.gates)
                    {
                        if (gate != gameObject)
                        {
                            //if the gate to drop is 2 qbit
                            if (gameObject.GetComponent<GateManager>().numQubits > 1)
                            {
                                //this gate hits another gate
                                if (point.x == gate.GetComponent<GateManager>().moment &&
                                    (point.y + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().qbit)
                                {
                                    canDrop = false;
                                }
                                //this gate hits a target
                                if (point.x == gate.GetComponent<GateManager>().moment &&
                                    (point.y + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().target)
                                {
                                    canDrop = false;
                                }
                                //this target hits a gate
                                if (point.x == gate.GetComponent<GateManager>().moment &&
                                    (point.y - 1 + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().qbit)
                                {
                                    canDrop = false;
                                }
                                //this target hits a gate
                                if (point.x == gate.GetComponent<GateManager>().moment &&
                                    (point.y - 1 + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) == gate.GetComponent<GateManager>().target)
                                {
                                    canDrop = false;
                                }
                                //if the target if off the edge
                                if ((point.y + (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1)) - 1 < 0)
                                {
                                    canDrop = false;
                                }
                            }

                            //if the gate to drop hits another gate
                            if (point.x == gate.GetComponent<GateManager>().moment && point.y == gate.GetComponent<GateManager>().qbit)
                            {
                                canDrop = false;
                            }
                            //checks for 2 qbit spots
                            if (gate.GetComponent<GateManager>().numQubits > 1)
                            {
                                if (point.x == gate.GetComponent<GateManager>().moment && point.y == gate.GetComponent<GateManager>().target)
                                {
                                    canDrop = false;
                                }
                            }
                        }
                    }
                }
                if (myTouch.phase == TouchPhase.Ended)
                {
                    if (canDrop && inCircuit)
                    {
                        float tqubit = point.y;
                        if (gameObject.GetComponent<GateManager>().numQubits > 1)
                        {
                            tqubit += (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1);
                        }
                        holding = false;
                        plop.playPlop();
                        gameManager.gateEntered = true;
                        gameObject.GetComponent<GateManager>().SetPositions((int)point.x, (int)tqubit);
                        if (gameObject.GetComponent<GateManager>().numQubits == 2)
                        {
                            gameObject.GetComponent<GateManager>().target = (int)tqubit - 1;
                        }
                    }

                    else if (holding && !inCircuit)
                    {
                        gameManager.gates.Remove(gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        #endif

            point.z = 0;

            transform.position = point;
            
        }
    }
#if UNITY_STANDALONE || UNITY_WEBPLAYER
    void OnMouseDown()
    {
        if (canDrop&&inCircuit)
        {
            float tqubit = point.y;
            if (gameObject.GetComponent<GateManager>().numQubits > 1)
            {
                tqubit += (float)0.5 * (gameObject.GetComponent<GateManager>().numQubits - 1);
            }
            holding = false;
            plop.playPlop();
            gameManager.gateEntered = true;
            gameObject.GetComponent<GateManager>().SetPositions((int)point.x , (int)tqubit);
            if (gameObject.GetComponent<GateManager>().numQubits==2)
            {
                gameObject.GetComponent<GateManager>().target = (int)tqubit - 1;
            }
        }

        if (holding&&!inCircuit)
        {
            gameManager.gates.Remove(gameObject);
            Destroy(gameObject);
        }
    }
#endif

}