using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class menuAnimation : MonoBehaviour {

    void Start()
    {
        if (!MainMenuController.movieSeen && this.gameObject.GetComponent<VideoPlayer>())
        {
            this.gameObject.GetComponent<VideoPlayer>().Play();
        }
    }

	public void end()
    {
        gameObject.SetActive(false);
        MainMenuController.movieSeen = true;
    }

}
