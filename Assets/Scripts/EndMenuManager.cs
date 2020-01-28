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
    private GameObject arrow;

    [SerializeField]
    private GameObject qubitResultText;

    [SerializeField]
    private GameObject qubitProbBackground;

    [SerializeField]
    private GameObject qubitsContainer;

    [SerializeField]
    private Button nextButton;

    List<int> qubitResults;

    private SettingsManager settings;  
    

    private List<Vector3> positions;
    private List<Vector3> arrowPositions;
    private List<Vector3> textPositionns;
    // Start is called before the first frame update
    void Start()
    {
        qubitResults = new List<int>();
        
        positions = new List<Vector3>();
        positions.Add(new Vector3(0, 500));
        positions.Add(new Vector3(0, 175));
        positions.Add(new Vector3(0, -150));

        arrowPositions = new List<Vector3>();
        arrowPositions.Add(new Vector3(261, 500));
        arrowPositions.Add(new Vector3(261, 175));
        arrowPositions.Add(new Vector3(261, -150));

        textPositionns = new List<Vector3>();
        textPositionns.Add(new Vector3(465, 500));
        textPositionns.Add(new Vector3(465, 175));
        textPositionns.Add(new Vector3(465, -150));

        nextButton.onClick.AddListener(NextButtonOnClick);


        settings = GameObject.Find("SettingsManager").GetComponent<SettingsManager>();
        int i = 0;
        foreach(float qubitProb in settings.qbitProbs)
        {
            GameObject chart = GameObject.Instantiate(qubitProbChart, qubitsContainer.transform.position, Quaternion.identity);
            chart.transform.SetParent(qubitsContainer.transform, false);
            chart.transform.position = qubitsContainer.transform.position + positions[i];
            chart.GetComponent<Image>().fillAmount = qubitProb;

            GameObject chartBackground = GameObject.Instantiate(qubitProbBackground, qubitsContainer.transform.position, Quaternion.identity);
            chartBackground.transform.SetParent(qubitsContainer.transform, false);
            chartBackground.transform.position = qubitsContainer.transform.position + positions[i];
            chartBackground.GetComponent<Image>().fillAmount = (1f - qubitProb);

            GameObject arrowImage = GameObject.Instantiate(arrow, qubitsContainer.transform.position, Quaternion.identity);
            arrowImage.transform.SetParent(qubitsContainer.transform, false);
            arrowImage.transform.position = qubitsContainer.transform.position + arrowPositions[i];

            if (qubitProb * 100 >= Random.Range(0, 100))
            {
                qubitResults.Add(1);
            }
            else
            {
                qubitResults.Add(0);
            }

            GameObject resultText = GameObject.Instantiate(qubitResultText, qubitsContainer.transform.position, Quaternion.identity);
            resultText.transform.SetParent(qubitsContainer.transform, false);
            resultText.transform.position = qubitsContainer.transform.position + textPositionns[i];
            resultText.GetComponentInChildren<Text>().text = qubitResults[i].ToString();

            i++;

     
               


        }
        int qubitSum = 0;
        foreach (int prob in qubitResults)
        {
            qubitSum += prob;
        }
        SettingsManager settingsScript = settings.GetComponent<SettingsManager>();
        if (qubitSum > ((float)settingsScript.numQubits)/2f)
        {
            GameObject.Find("WinnerText").GetComponent<Text>().text = "Sender!";
            GameObject.Find("WinnerText").GetComponent<Text>().color = new Color(181f/255f, 221f/255f, 209f/255f);
        }
        else if (qubitSum < ((float)settingsScript.numQubits) / 2f)
        {
            GameObject.Find("WinnerText").GetComponent<Text>().text = "Blocker!";
            GameObject.Find("WinnerText").GetComponent<Text>().color = new Color(230f / 255f, 146f / 255f, 136f / 255f);
        }
        else if (qubitSum == ((float)settingsScript.numQubits) / 2f)
        {
            GameObject.Find("WinnerText").GetComponent<Text>().text = "Its a tie!";
        }


        SceneManager.MoveGameObjectToScene(GameObject.Find("SettingsManager"), SceneManager.GetActiveScene());

    }

    void NextButtonOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
