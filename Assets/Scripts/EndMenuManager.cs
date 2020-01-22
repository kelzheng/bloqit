using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject qubitProbChart;
    [SerializeField]
    private GameObject qubitsContainer;
    private SettingsManager settings;   

    private List<Vector3> positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        positions.Add(new Vector3(0, 0));
        positions.Add(new Vector3(0, -75));
        positions.Add(new Vector3(0, -150));


        settings = GameObject.Find("SettingsManager").GetComponent<SettingsManager>();
        int i = 0;
        foreach(float qubitProb in settings.qbitProbs)
        {
            GameObject chart = GameObject.Instantiate(qubitProbChart, qubitsContainer.transform.position, Quaternion.identity);
            chart.transform.SetParent(qubitsContainer.transform, false);
            chart.transform.position = qubitsContainer.transform.position + positions[i];
            chart.GetComponent<Image>().fillAmount = qubitProb;
            i++;
        }        
    }
}
