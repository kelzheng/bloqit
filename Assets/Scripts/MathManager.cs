using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using UnityEngine;

public class MathManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Matrix<double> A = DenseMatrix.OfArray(new double[,] {
        {1,1,1,1},
        {1,2,3,4},
        {4,3,2,1}});
        Vector<double>[] nullspace = A.Kernel();



        Debug.Log((A * (2 * nullspace[0] - 3 * nullspace[1])));

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
