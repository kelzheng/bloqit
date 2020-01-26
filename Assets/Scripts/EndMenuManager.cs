using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject qubitProbChart;

    [SerializeField]
    private GameObject qubitsContainer;

    [SerializeField]
    private Button nextButton;

    private SettingsManager settings;  
    

    private List<Vector3> positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        positions.Add(new Vector3(0, 500));
        positions.Add(new Vector3(0, 175));
        positions.Add(new Vector3(0, -150));

        nextButton.onClick.AddListener(NextButtonOnClick);


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
        SceneManager.MoveGameObjectToScene(GameObject.Find("SettingsManager"), SceneManager.GetActiveScene());

    }

    void NextButtonOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
