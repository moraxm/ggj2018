using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeup : MonoBehaviour {

    public float TimeToAdd = 10.0f;

    private GameManager gameManager = null;

	// Use this for initialization
	void Start ()
    {
        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        if (!gameManagerObj)
        {
            Debug.LogError("[Timeup.Start] Error. GameManager not found");
            return;
        }
        gameManager = gameManagerObj.GetComponent<GameManager>();
        if (!gameManager)
        {
            Debug.LogError("[Timeup.Start] Error. GameManager has not GameManager component");
            return;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.IncreaseTime(TimeToAdd);
        }
    }
}
