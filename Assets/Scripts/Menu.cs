using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene("Introduction");
    }

    public void QuitGame()
    {
        //Debug.Log("Game has exited");
        Application.Quit();
    }
}
