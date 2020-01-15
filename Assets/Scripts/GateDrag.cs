using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDrag : MonoBehaviour
{
    bool holding = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(holding)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0;
            transform.position = point;
        }
    }

    void OnMouseDown()
    {
        holding = false;
    }

}