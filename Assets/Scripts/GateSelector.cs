using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSelector : MonoBehaviour
{

    [SerializeField]
    GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Instantiate(gate, transform.position, Quaternion.identity);
    }

}
