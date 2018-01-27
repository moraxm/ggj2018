using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Button exitButton = null;

    void Awake()
    {
        #if !UNITY_STANDALONE_WIN && !UNITY_STANDALONE_OSX || UNITY_EDITOR
        if (exitButton)
        {
            exitButton.interactable = false;
        }
        #endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {

    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX && !UNITY_EDITOR
            Application.Quit();
        #endif
    }
}
