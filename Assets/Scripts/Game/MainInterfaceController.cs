using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInterfaceController : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ConfigurePlayersInterface(GlobalInputManager globalInputManager)
    {
        // Find PlayersInfo structure on Canvas
        Transform playersInfoTransform = transform.Find("PlayersInfo");
        if (!playersInfoTransform)
        {
            Debug.LogError("[MainInterfaceController.ConfigurePlayersInterface] Error. PlayersInfo not found");
            return;
        }
        GameObject playersInfo = playersInfoTransform.gameObject;

        // For each player, find that player info on Canvas and enable it
        for (uint i = 1; i <= GameManager.numberOfPlayers; ++i)
        {
            Transform playerInfoTransform = playersInfo.transform.Find("InfoPlayer" + i);
            if (!playerInfoTransform)
            {
                Debug.LogError("[MainInterfaceController.ConfigurePlayersInterface] Error. InfoPlayer" + i + " not found");
                return;
            }
            playerInfoTransform.gameObject.SetActive(true);

            Transform actionsTransform = playerInfoTransform.Find("Actions");
            if (!actionsTransform)
            {
                Debug.LogError("[MainInterfaceController.ConfigurePlayersInterface] Error. Actions not found for InfoPlayer" + i);
                return;
            }

            int lastActionChecked = -1;
            // For each action of that player
            for (uint j = 1; j <= 4; ++j)
            {
                // Locate UI sprite
                Transform actionTransform = actionsTransform.Find("Action" + j);
                if (!actionTransform)
                {
                    Debug.LogError("[MainInterfaceController.ConfigurePlayersInterface] Error. Action" + j + " not found");
                    return;
                }

                Image actionImage = actionTransform.gameObject.GetComponent<Image>();
                if (!actionImage)
                {
                    Debug.LogError("[MainInterfaceController.ConfigurePlayersInterface] Error. Action" + j + " does not have Image component");
                    return;
                }

                // Which action needs to be checkes now?
                if (lastActionChecked < 0 && globalInputManager.players[i - 1].m_L2Action != null) // Action exists, assign its icon to this UI sprite
                {
                    actionTransform.gameObject.SetActive(true);
                    actionImage.sprite = globalInputManager.players[i - 1].m_L2Action.spriteUI;
                    lastActionChecked = 0;
                }
                else if (lastActionChecked < 1 && globalInputManager.players[i - 1].m_R2Action != null)
                {
                    actionTransform.gameObject.SetActive(true);
                    actionImage.sprite = globalInputManager.players[i - 1].m_R2Action.spriteUI;
                    lastActionChecked = 1;
                }
                else if (lastActionChecked < 2 && globalInputManager.players[i - 1].m_UPAction != null)
                {
                    actionTransform.gameObject.SetActive(true);
                    actionImage.sprite = globalInputManager.players[i - 1].m_UPAction.spriteUI;
                    lastActionChecked = 2;
                }
                else if (lastActionChecked < 3 && globalInputManager.players[i - 1].m_DOWNAction != null)
                {
                    actionTransform.gameObject.SetActive(true);
                    actionImage.sprite = globalInputManager.players[i - 1].m_DOWNAction.spriteUI;
                    lastActionChecked = 3;
                }
                else if (lastActionChecked < 4 && globalInputManager.players[i - 1].m_RIGHTAction != null)
                {
                    actionTransform.gameObject.SetActive(true);
                    actionImage.sprite = globalInputManager.players[i - 1].m_RIGHTAction.spriteUI;
                    lastActionChecked = 4;
                }
                else if (lastActionChecked < 5 && globalInputManager.players[i - 1].m_LEFTAction != null)
                {
                    actionTransform.gameObject.SetActive(true);
                    actionImage.sprite = globalInputManager.players[i - 1].m_LEFTAction.spriteUI;
                    lastActionChecked = 5;
                    break; // Everything checked
                }
            }
        }
    }
}
