using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public const uint MAX_PLAYERS = 3;

    private MainInterfaceController mainInterfaceController = null;

	// Use this for initialization
	void Start ()
    {
        GameObject mainInterface = GameObject.FindGameObjectWithTag("MainInterface");
        if (!mainInterface)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterface not found");
            return;
        }

        mainInterfaceController = mainInterface.GetComponent<MainInterfaceController>();
        if (!mainInterfaceController)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterface has not MainInterfaceController");
            return;
        }

        // Configurate interface for the current number of players
        mainInterfaceController.ConfigurePlayersInterface(3);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
