using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string gateType;
    public int numQubits;
    int[] targetQbits;
    //ex. toffoli gate is [control, control, target]
    
    void Start()
    {
        targetQbits = new int[numQubits];
    }

    void SetQbits(int[] qubits)
    {
        targetQbits = qubits;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
