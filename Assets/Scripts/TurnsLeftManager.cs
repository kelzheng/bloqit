using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnsLeftManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Text text;
    void Awake()
    {
        text = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    public void updateTurns(int turns)
    {
        this.text.text = "Turns Left: "+ turns.ToString();
    }

    void Update()
    {
        
    }
}
