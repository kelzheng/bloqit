using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDrag : MonoBehaviour
{
    bool holding = true;
    BoardManager board;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("GameManager").GetComponent<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(holding)
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if ((point.x >=-0.5 && point.x <= board.columns+0.45-1) && (point.y >= -0.5 && point.y <= board.rows+0.45-1))
            {
                point.x = Mathf.RoundToInt(point.x);
                point.y = Mathf.RoundToInt(point.y);
                Debug.Log("x:" + point + " y:" + point.y);
            }
            point.z = 0;
            transform.position = point;
        }
    }

    void OnMouseDown()
    {
        holding = false;
    }

}