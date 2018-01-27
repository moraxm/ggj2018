using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInterfaceController : MonoBehaviour {

    public Sprite MoveUp = null;
    public Sprite MoveDown = null;
    public Sprite MoveLeft = null;
    public Sprite MoveRight = null;
    public Sprite Ladder = null;
    public Sprite Door = null;
    public Sprite Use = null;

	// Use this for initialization
	void Awake ()
    {
        if (!MoveUp || !MoveDown || !MoveLeft || !MoveRight || !Ladder || !Door || !Use)
        {
            Debug.LogError("[MainInterfaceController.Start] Error. An action sprite has not been configured via editor");
            return;
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ConfigurePlayersInterface(uint numPlayers)
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
        for (uint i = 1; i <= numPlayers; ++i)
        {
            Transform playerInfoTransform = playersInfo.transform.Find("InfoPlayer" + i);
            if (!playerInfoTransform)
            {
                Debug.LogError("[MainInterfaceController.ConfigurePlayersInterface] Error. InfoPlayer" + i + " not found");
                return;
            }
            GameObject playerInfo = playerInfoTransform.gameObject;
            playerInfo.SetActive(true);
        }
    }
}
