using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class menuMusic : MonoBehaviour {
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
        if (Application.loadedLevelName == "Desert 1")
        {
            GetComponent<AudioSource>().Stop();
            AudioBegin = false;
            Destroy(gameObject);
        }
    }
}
