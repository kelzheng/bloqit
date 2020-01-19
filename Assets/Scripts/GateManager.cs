using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string gateType;
    public int numQubits;
    int[] targetQbits;
    int moment;
    //ex. toffoli gate is [control, control, target]
    
    void Start()
    {
        targetQbits = new int[numQubits];
    }

    void SetQbits(int[] qubits)
    {
        targetQbits = qubits;
    }

    public void setPositions(int moment, int[] tqbits)
    {
        this.moment = moment;
        this.targetQbits = tqbits;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
