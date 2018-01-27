using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject mainInterface = null;
    public GameObject players = null;

    public static uint numberOfPlayers = 2;

	// Use this for initialization
	void Start ()
    {
        if (!mainInterface)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterface object not found");
            return;
        }

        if (!players)
        {
            Debug.LogError("[GameManager.Start] Error. Players object not found");
            return;
        }

        // Configurate number of players
        MainInterfaceController mainInterfaceController = mainInterface.GetComponent<MainInterfaceController>();
        if (!mainInterfaceController)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterface object does not have MainInterfaceController");
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
