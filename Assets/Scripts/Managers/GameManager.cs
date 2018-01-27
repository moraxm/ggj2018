using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private GameObject mainInterface = null;
    private GameObject playersCharacters = null;

    public static uint numberOfPlayers = 2;

	// Use this for initialization
	void Start ()
    {
        // Locate MainInterface object
        Transform mainInterfaceTransform = transform.Find("MainInterface");
        if (!mainInterfaceTransform)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterface not found");
            return;
        }
        mainInterface = mainInterfaceTransform.gameObject;

        // Locate PlayersCharacters object
        Transform playersCharactersTransform = transform.Find("PlayersCharacters");
        if (!playersCharactersTransform)
        {
            Debug.LogError("[GameManager.Start] Error. PlayersCharacters not found");
            return;
        }
        playersCharacters = playersCharactersTransform.gameObject;

        // Configurate number of players
        MainInterfaceController mainInterfaceController = mainInterface.GetComponent<MainInterfaceController>();
        if (!mainInterfaceController)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterface object does not have MainInterfaceController");
            return;
        }

        Transform players = playersCharacters.transform.Find("Players");
        if (!players)
        {
            Debug.LogError("[GameManager.Start] Error. Players not found");
            return;
        }

        GlobalInputManager globalInputManager = players.gameObject.GetComponent<GlobalInputManager>();
        if (!globalInputManager)
        {
            Debug.LogError("[GameManager.Start] Error. Players object does not have GlobalInputManager");
            return;
        }

        // Set selected number of players
        mainInterfaceController.ConfigurePlayersInterface(numberOfPlayers);
        globalInputManager.numberOfPlayers = numberOfPlayers;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
