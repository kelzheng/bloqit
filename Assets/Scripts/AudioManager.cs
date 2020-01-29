using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioSource audioSource;

    [SerializeField]
    AudioClip plop;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPlop()
    {
        audioSource.PlayOneShot(plop);
    }
}
