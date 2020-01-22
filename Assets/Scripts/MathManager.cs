using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
//using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics;
using System.Numerics;
using UnityEngine;

public class MathManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager gameManager;
    BoardManagerV2 board;


    void Start()
    {
        board = GameObject.Find("BoardManager").GetComponent<BoardManagerV2>();
        gameManager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;
        //MathNet.Numerics.LinearAlgebra.Vector<Complex> u = MathNet.Numerics.LinearAlgebra.Vector<Complex>.Build.Random(10);
/*        Matrix<Complex32> A = DenseMatrix.OfArray(new Complex32[,] {
        {new Complex32(4,5),new Complex32(1,2)},
        {new Complex32(4,5),new Complex32(1,2)}
        });



        Debug.Log((A));
        Debug.Log(A[0, 0].Real);*/

    }

    public void CompileForMath()
    {
        string[,] gateArray = new string[board.rows, board.columns];
        //Debug.Log(gateArray.Length);
        
        foreach (GameObject gate in gameManager.gates)
        {

            //Debug.Log("[" + gate.GetComponent<GateManager>().qbit + "," + gate.GetComponent<GateManager>().moment + "]" + gate.GetComponent<GateManager>().gateType
            if (gate.GetComponent<GateManager>().numQubits==2)
            {
                gateArray[gate.GetComponent<GateManager>().target, gate.GetComponent<GateManager>().moment] = gate.GetComponent<GateManager>().gateType+"t";
            }
            gateArray[gate.GetComponent<GateManager>().qbit, gate.GetComponent<GateManager>().moment] = gate.GetComponent<GateManager>().gateType;

            
        }
        string displayString = "";
        for (int i = 0; i < board.rows; i++)
        {
            for (int j = 0; j < board.columns; j++)
            {
                displayString += gateArray[i, j];
            }
            displayString += '\n';
        }
        Debug.Log(displayString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
