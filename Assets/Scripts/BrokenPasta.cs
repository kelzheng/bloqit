using UnityEngine;
using MathNet.Numerics; // for Complex32 values in gates
using MathNet.Numerics.LinearAlgebra.Complex32; // for gates
using System.Collections.Generic; // for List

public class BrokenPasta : MonoBehaviour
{
    GameManager gameManager;
    BoardManagerV2 board;

    void Start() // Method to test Debug.Log()s in unity //
    {
        board = GameObject.Find("BoardManager").GetComponent<BoardManagerV2>();
        gameManager = GameObject.Find("GameManager").GetComponent("GameManager") as GameManager;

        //string[,] TestCircuit = { { "h", "t", "h", "i", "i", "i" }, { "i", "i", "i", "i", "i", "i" } }; // a test circuit
        //var EvaluatedMoments = EvaluateMoment(TestCircuit); // evaluate the moments in the circuit, return one equivalent moments multiplied together
        //var FinalState = CalcState(EvaluatedMoments); // multiply the state by the moments
        //qbitProbs(EvaluatedMoments);
        //Debug.Log(FinalState); // return the final state
    }

    public string[,] CompileForMath()
    {
        string[,] gateArray = new string[board.rows, board.columns];
        for (int i = 0; i < board.rows; i++)
        {
            for (int j = 0; j < board.columns; j++)
            {
                gateArray[i, j]="i";
            }   
        }


        //Debug.Log(gateArray.Length);

        foreach (GameObject gate in gameManager.gates)
        {

            //Debug.Log("[" + gate.GetComponent<GateManager>().qbit + "," + gate.GetComponent<GateManager>().moment + "]" + gate.GetComponent<GateManager>().gateType
            if (gate.GetComponent<GateManager>().numQubits == 2)
            {
                gateArray[gate.GetComponent<GateManager>().target, gate.GetComponent<GateManager>().moment] = gate.GetComponent<GateManager>().gateType + "t";
            }
            gateArray[gate.GetComponent<GateManager>().qbit, gate.GetComponent<GateManager>().moment] = gate.GetComponent<GateManager>().gateType;


        }
/*        string displayString = "";
        for (int i = 0; i < board.rows; i++)
        {
            for (int j = 0; j < board.columns; j++)
            {
                displayString += gateArray[i, j];
            }
            displayString += '\n';
        }
        Debug.Log(displayString);*/
        return gateArray;
    }

    public DenseMatrix MatchGate(string gate) // Method to take string gate name -> 2x2 or 4x4 DenseMatrix of equivalent gate //
    {
        // 1. Unary Gates, 2x2
        // 1.A Pauli Matricies (rotate on x, y and z matrices 180 degrees)
        // X operator, NOT operator, bit flip, sigma_x
        var X = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(0,0),new Complex32(1,0)},
             {new Complex32(1,0),new Complex32(0,0)}});
        // Y operator, sigma_y
        var Y = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(0,0),new Complex32(0,-1)},
             {new Complex32(0,1),new Complex32(0,0)}});
        // Z operator, phase flip operator, flips by pi rad aka 180 deg)
        var Z = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(-1,0)}});
        // I operator, identity
        var I = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0)}});

        // 1.B Special cases of phase shift operator, R_phi
        // S operator, phi = pi/2, rotate on z axis by 90 deg
        var S = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,1)}});
        // T operator, phi = pi/2, rotate on z axis by 45 deg
        var T = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),Complex32.FromPolarCoordinates(1,Mathf.PI/4)}});

        // 1.C Hadamard operator, takes qubit from definite computational basis to a superposition of two states
        var H = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32((1/Mathf.Sqrt(2)),0),new Complex32((1/Mathf.Sqrt(2)),0)},
             {new Complex32((1/Mathf.Sqrt(2)),0),new Complex32(-(1/Mathf.Sqrt(2)),0)}});

        // 2. Binary Operators, 4x4
        // 2.A Swap operator, swaps states of two qubits
        var SWAP = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)}});
        // 2.B CNOT operator, if control qubit (1st q) is 0, do nothing to target qubit (2nd q),
        // but if control qubit is 1, apply NOT operator to target qubit
        var CNOT = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)}});
        // 2.C CZ operator, if control qubit (1st q) is 0, do nothing to target qubit (2nd q),
        // but if control qubit is 1, apply Z operator to target qubit
        // is symmetric so same result if either qubit is target/control
        var CZ = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(-1,0)}});
        //Debug.Log((X)); Debug.Log((Y)); Debug.Log((Z)); Debug.Log((S)); Debug.Log((T)); Debug.Log((H));

        // 3. Match the gate


        //int numQubits = 2;
        //int rows = numQubits;
        //int columns = gate.Length/numQubits;

        if (gate == "x")
        {
            return X;
        }

        if (gate == "y")
        {
            return Y;
        }

        if (gate == "z")
        {
            return Z;
        }

        if (gate == "i")
        {
            return I;
        }

        if (gate == "s")
        {
            return S;
        }

        if (gate == "t")
        {
            return T;
        }

        if (gate == "h")
        {
            return H;
        }

        if (gate == "cnot" || gate == "cnott")
        {
            return CNOT;
        }

        if (gate == "cz" || gate == "czt")
        {
            return CZ;
        }

        if (gate == "swap" || gate == "swapt")
        {
            return SWAP;
        }
        else
        {
            return null;
        }
    }

    float[] qbitProbs(DenseMatrix state)
    {
        float[] probs= new float[board.rows];
        //Debug.Log(state);
        state = (DenseMatrix)state.ConjugateTransposeAndMultiply(state);
        //Debug.Log(state);

        if (board.rows == 1)
        {
            probs[0] = state[1, 1].Real;
        }
        if (board.rows == 2)
        {
            probs[0] = state[1, 1].Real+ state[3, 3].Real;
            probs[1] = state[2, 2].Real + state[3, 3].Real;
        }
        //Debug.Log("q1: "+probs[0]+" Q2: "+probs[1]);
        return probs;



        //Debug.Log(state);
    }

    public DenseMatrix EvaluateMoment(string[,] Circuit) // Method to take numQubitsx(Circuit.Length/numQubits) string array of gate names -> 2x2 or 4x4 DenseMatrix of equivalent gate with MatchGate method -> 4x4 DenseMatrix of all moments multiplied together //
    {
        // Circuit dimensions
        int numQubits = board.rows;
        int rows = numQubits;
        int columns = board.columns;

        // Initialize gates to be tensored
        List<DenseMatrix> Moments = new List<DenseMatrix>();
        string gate;
        DenseMatrix gateOne = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(-1,0)}});
        DenseMatrix gateTwo = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(-1,0)}});
        DenseMatrix TensoredMoment = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)}});
        DenseMatrix FinalTensoredMoments = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)}});

        // Gates for skip condition
        var SWAP = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)}});
        var CNOT = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)}});
        var CZ = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(-1,0)}});

        var ket0 = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0)},
         {new Complex32(0,0)}});
        var ket00 = ket0.KroneckerProduct(ket0);
        var state = ket00;


        // Match gates in each moment and tensor together
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (j == 0)
                {
                    gate = Circuit[j, i];
                    gateOne = MatchGate(gate);
                }
                if (j == 1)
                {
                    gate = Circuit[j, i];
                    gateTwo = MatchGate(gate);
                }

            }
            if (numQubits == 2)
            {
                // Skip condition for SWAP, CNOT, CZ
                if (gateOne.Equals(SWAP) || gateOne.Equals(CNOT) || gateOne.Equals(CZ) || gateTwo.Equals(SWAP) || gateTwo.Equals(CNOT) || gateTwo.Equals(CZ))
                {
                    //Debug.Log("this has run");
                    TensoredMoment = gateOne;
                    Moments.Add(TensoredMoment);
                }
                else
                {
                    TensoredMoment = (DenseMatrix)gateOne.KroneckerProduct(gateTwo);
                    Moments.Add(TensoredMoment);
                }
            }
                
            //Debug.Log(gateOne);
            //Debug.Log(gateTwo);
            //Debug.Log(TensoredMoment);
            state = TensoredMoment.Multiply(state);
            //Debug.Log(state);
        }

        // Multiply together all moments for FinalTensoredMoments
/*        for (int idx = 0; idx < rows; idx++)
        {
            FinalTensoredMoments = (DenseMatrix) FinalTensoredMoments.Multiply(Moments[idx]);
        }*/

        return (DenseMatrix) state;
    }


    public DenseMatrix CalcState(DenseMatrix FinalTensoredMoments) // Method to take 4x4 DenseMatrix of FinalTensoredMoments -> 4x1 DenseMatrix of final state, assuming |00> initial state
    {
        // States
        // 1-qubit basis states <0| and <1|
        var bra0 = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0), new Complex32(0,0) }});
        var bra1 = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(0,0), new Complex32(1,0) }});
        // Some kets |0> and |1>
        var ket0 = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0)},
         {new Complex32(0,0)}});
        var ket1 = DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(0,0)},
         {new Complex32(1,0)}});
        // 2-qubit basis states |00> |11> |01> |10>
        // note: Kronecker Product is tensor-product
        var ket00 = ket0.KroneckerProduct(ket0);
        var ket11 = ket1.KroneckerProduct(ket1);
        var ket01 = ket0.KroneckerProduct(ket1);
        var ket10 = ket1.KroneckerProduct(ket0);

        // Mutltiply state by FinalTensoredMoments
        //Debug.Log(FinalTensoredMoments);
        var state = (DenseMatrix)FinalTensoredMoments.Multiply(ket00);

        // Return final state
        return state;
    }



    public float[] getProbs(string [,] circuit)
    {
        var state = EvaluateMoment(circuit);
        float [] probs = qbitProbs(state);
        //Debug.Log(finalState);
        return probs;
    }
    //var EvaluatedMoments = EvaluateMoment(TestCircuit); // evaluate the moments in the circuit, return one equivalent moments multiplied together
    //var FinalState = CalcState(EvaluatedMoments); // multiply the state by the moments
    //Debug.Log(FinalState); // return the final state
}