using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingameMusic : MonoBehaviour {

    static bool AudioBegin = false;
    void Awake()
    {
        if (!AudioBegin)
        {
            GetComponent<AudioSource>().Play();
            //audio.Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }
    void Update()
    {
        if (Application.loadedLevelName == "Main Menu")
        {
            GetComponent<AudioSource>().Stop();
            AudioBegin = false;
            Destroy(gameObject);
        }
    }
}
