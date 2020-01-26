using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InstructionsMenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Button nextButton;

    [SerializeField]
    private GameObject settings;


    void Start()
    {
        nextButton.onClick.AddListener(NextButtonOnClick);
        settings = GameObject.Find("SettingsManager");
        DontDestroyOnLoad(settings);
    }

    void NextButtonOnClick()
    {
        SceneManager.LoadScene("BoardDev");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
