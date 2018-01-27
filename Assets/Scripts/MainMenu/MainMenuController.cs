using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Button exitButton = null;

    private GameObject playerSelections = null;

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
        Transform playerSelectionsTrans = transform.Find("SelectPlayers");
        if (!playerSelectionsTrans)
        {
            Debug.LogError("[MainMenuController.Start] Error. SelectPlayers not found");
            return;
        }

        playerSelections = playerSelectionsTrans.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameButton()
    {
        playerSelections.SetActive(true);
    }

    public void Select2Players()
    {
        playerSelections.SetActive(false);
        GameManager.numberOfPlayers = 2;
        SceneManager.LoadScene("Game");
    }

    public void Select3Players()
    {
        playerSelections.SetActive(false);
        GameManager.numberOfPlayers = 3;
        SceneManager.LoadScene("Game");
    }

    public void SelectPlayersClose()
    {
        playerSelections.SetActive(false);
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
