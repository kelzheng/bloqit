using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string gateType;
    public int numQubits;
    public int qbit;
    public int target;
    public int moment;
    //ex. toffoli gate is [control, control, target]
    
    void Start()
    {
        //qbit = new int[numQubits];
    }

    /*void SetQbits(int[] qubits)
    {
        qbit = qubits;
    }*/

    public void SetPositions(int moment, int tqbit)
    {
        this.moment = moment;
        this.qbit = tqbit;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
