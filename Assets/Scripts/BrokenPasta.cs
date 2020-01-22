using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Complex32;


using UnityEngine;
using MathNet.Numerics;

public class BrokenPasta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 1. Unary Gates
        // 1.A Pauli Matricies (rotate on x, y and z matrices 180 degrees)
        // X operator, NOT operator, bit flip, sigma_x
        var X = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(0,0),new Complex32(1,0)},
             {new Complex32(1,0),new Complex32(0,0)}});
        // Y operator, sigma_y
        var Y = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(0,0),new Complex32(0,-1)},
             {new Complex32(0,1),new Complex32(0,0)}});
        // Z operator, phase flip operator, flips by pi rad aka 180 deg)
        var Z = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(-1,0)}});
        // I operator, identity
        var I = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0)}});

        // 1.B Special cases of phase shift operator, R_phi
        // S operator, phi = pi/2, rotate on z axis by 90 deg
        var S = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,1)}});
        // T operator, phi = pi/2, rotate on z axis by 45 deg
        var T = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),Complex32.FromPolarCoordinates(1,Mathf.PI/4)}});

        // 1.C Hadamard operator, takes qubit from definite computational basis to a superposition of two states
        var H = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32((1/Mathf.Sqrt(2)),0),new Complex32((1/Mathf.Sqrt(2)),0)},
             {new Complex32((1/Mathf.Sqrt(2)),0),new Complex32(-(1/Mathf.Sqrt(2)),0)}});

        // 2. Binary Operators
        // 2.A Swap operator, swaps states of two qubits
        var SWAP = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)}});
        // 2.B CNOT operator, if control qubit (1st q) is 0, do nothing to target qubit (2nd q),
        // but if control qubit is 1, apply NOT operator to target qubit
        var CNOT = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(1,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)}});
        // 2.C CZ operator, if control qubit (1st q) is 0, do nothing to target qubit (2nd q),
        // but if control qubit is 1, apply Z operator to target qubit
        // is symmetric so same result if either qubit is target/control
        var CZ = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0),new Complex32(0,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(1,0),new Complex32(0,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(1,0),new Complex32(0,0)},
             {new Complex32(0,0),new Complex32(0,0),new Complex32(0,0),new Complex32(-1,0)}});
        //Debug.Log((X)); Debug.Log((Y)); Debug.Log((Z)); Debug.Log((S)); Debug.Log((T)); Debug.Log((H));


        //        _.-- -._ /\\
        //    ./ '       "--`\//
        //  ./              o \
        // /./\  )______   \__ \
        //./  / /\ \   | \ \  \ \
        //   / /  \ \  | |\ \  \7
        //    "     "    "  "

        // 3. States
        // try to learn how to tensor multipy from here https://iridium.mathdotnet.com/api/MathNet.Numerics.LinearAlgebra/Matrix.htm
        var ket0 = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
            {{new Complex32(1,0)},
             {new Complex32(0,0)}});

        //var nood = ket0.TensorMultiply(ket0);
        //Debug.Log((nood));

        //Matrix<double> A = DenseMatrix.OfArray(new double[,] {
        //{1,1,1,1},
        //{1,2,3,4},
        //{4,3,2,1}});
        //Vector<double>[] nullspace = A.Kernel();

        //// verify: the following should be approximately (0,0,0)
        //Debug.Log((A * (2 * nullspace[0] - 3 * nullspace[1])));

        //var M = Matrix<double>.Build;
        //var C = Matrix<Complex32>.Build;

        //var X = M.DenseOfArray(new[,] {{ 0.0,  1.0 },
        //                               { 1.0, 0.0 }});

        //var A = MathNet.Numerics.LinearAlgebra.Complex32.DenseMatrix.OfArray(new Complex32[,]
        //    {{new Complex32(1,0),new Complex32(0,0)},
        //      {new Complex32(0,0),new Complex32(1,0)}});
        //Debug.Log((A));


        //multiplication for later from https://numerics.mathdotnet.com/api/MathNet.Numerics.LinearAlgebra.Complex/Matrix.htm#Multiply
        //var tom = H.Multiply(H);
        //Debug.Log((tom));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
