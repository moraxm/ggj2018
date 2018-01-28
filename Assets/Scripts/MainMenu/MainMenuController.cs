﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Button exitButton = null;

    GameObject button2Players = null;
    GameObject button3Players = null;

    void Awake()
    {
        #if !UNITY_STANDALONE_WIN && !UNITY_STANDALONE_OSX || UNITY_EDITOR
        if (exitButton)
        {
            exitButton.interactable = false;
        }
        #endif
    }

    void Start()
    {
        UtilSound.instance.PlaySound("MainMenu", loop: true);
        Transform playerSelectionsTrans = transform.Find("ButtonPanel/SelectPlayers");
        if (!playerSelectionsTrans)
        {
            Debug.LogError("[MainMenuController.Start] Error. SelectPlayers not found");
            return;
        }

        Transform button2PlayersTransform = transform.Find("ButtonPanel/SelectPlayers/Image/2PlayersButton");
        Transform button3PlayersTransform = transform.Find("ButtonPanel/SelectPlayers/Image/3PlayersButton");

        if (!button2PlayersTransform || !button3PlayersTransform)
        {
            Debug.LogError("[MainMenuController.Start] Error. 2PlayersButton or 3PlayersButton not found");
            return;
        }

        button2Players = button2PlayersTransform.gameObject;
        button3Players = button3PlayersTransform.gameObject;

        // Initialize player selection in HUD
        if (button2Players.GetComponent<Image>() && button3Players.GetComponent<Image>())
        {
            if (GameManager.numberOfPlayers == 2)
            {
                Color color = button2Players.GetComponent<Image>().color;
                button2Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1.0f);
                color = button3Players.GetComponent<Image>().color;
                button3Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.0f);
            }
            else if (GameManager.numberOfPlayers == 3)
            {
                Color color = button2Players.GetComponent<Image>().color;
                button2Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.0f);
                color = button3Players.GetComponent<Image>().color;
                button3Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameButton()
    {
        UtilSound.instance.StopSound("MainMenu");
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene("Level1");
    }

    public void Select2Players()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        GameManager.numberOfPlayers = 2;
        if (button2Players.GetComponent<Image>() && button3Players.GetComponent<Image>())
        {
            Color color = button2Players.GetComponent<Image>().color;
            button2Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1.0f);
            color = button3Players.GetComponent<Image>().color;
            button3Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.0f);
        }
    }

    public void Select3Players()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        GameManager.numberOfPlayers = 3;
        if (button2Players.GetComponent<Image>() && button3Players.GetComponent<Image>())
        {
            Color color = button2Players.GetComponent<Image>().color;
            button2Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.0f);
            color = button3Players.GetComponent<Image>().color;
            button3Players.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1.0f);
        }
    }

    public void Credits()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene("Credits");
    }

    public void ExitGame()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX && !UNITY_EDITOR
            Application.Quit();
        #endif
    }
}
