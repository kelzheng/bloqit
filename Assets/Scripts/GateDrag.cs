using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: make actual collision and not a janky versio

public class GateDrag : MonoBehaviour
{
    bool holding = true;
    bool canDrop = true;
    bool inCircuit = false;
    BoardManager board;
    GameManager gameManager;
    //   Collider2D[] results;
    //   GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("GameManager").GetComponent<BoardManager>();
        //results = new Collider2D[board.columns * board.rows];
        gameManager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;

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

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
                
                foreach(GameObject gate in gameManager.gates)
                {
                    if(gate != gameObject)
                        if (point.x == gate.transform.position[0] && point.y == gate.transform.position[1])
                        {
                            canDrop = false;
                        }
                }

                                
                
            }
            point.z = 0;

            transform.position = point;
            
        }
    }

    void OnMouseDown()
    {
  
        if (canDrop&&inCircuit)
        {
            holding = false;
            gameManager.gateEntered = true;
        }

        if (holding&&!inCircuit)
        {
            gameManager.gates.Remove(gameObject);
            Destroy(gameObject);
        }
        
    }

}