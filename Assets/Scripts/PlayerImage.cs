using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerImage : MonoBehaviour
{
    // Start is called before the first frame update
    Text playerText;

    [SerializeField]
    Sprite sender;

    [SerializeField]
    Sprite blocker;
    void Awake()
    {
        playerText = gameObject.GetComponentInChildren<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePlayer(string newPlayer)
    {

        playerText.text = newPlayer;
        if (newPlayer == "Sender")
        {
            gameObject.GetComponent<Image>().sprite = sender;
        }
        if (newPlayer == "Blocker")
        {
            gameObject.GetComponent<Image>().sprite = blocker;
        }
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        float currentScale = 1.3f;
        float scaleStep = 0.02f;

        while (currentScale > 1)
        {
            gameObject.transform.localScale =  new Vector3(currentScale, currentScale, currentScale);
            currentScale -= scaleStep;
            yield return new WaitForEndOfFrame();
        }

    }
}
