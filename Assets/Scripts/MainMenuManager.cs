using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private Button oneQubit;
    [SerializeField]
    private Button twoQubit;
    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private GameObject settings;

    private SettingsManager settingsScript;


    void Start()
    {
        oneQubit.onClick.AddListener(OneQubitOnClick);
        twoQubit.onClick.AddListener(TwoQubitOnClick);
        exitButton.onClick.AddListener(ExitButtonOnClick);
        settingsScript = settings.GetComponent<SettingsManager>();
        DontDestroyOnLoad(settings);
    }

    void OneQubitOnClick()
    {
        settingsScript.numQubits = 1;
        SceneManager.LoadScene("Instructions");
    }

    void TwoQubitOnClick()
    {
        settingsScript.numQubits = 2;
        SceneManager.LoadScene("Instructions");
    }
    void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
