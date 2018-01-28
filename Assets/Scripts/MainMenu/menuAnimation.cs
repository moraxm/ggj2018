using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class menuAnimation : MonoBehaviour {

    public static bool movieSeen = false;

    void Start()
    {
        if (!menuAnimation.movieSeen && this.gameObject.GetComponent<VideoPlayer>())
        {
            this.gameObject.GetComponent<VideoPlayer>().Play();
        }
    }

	public void end()
    {
        gameObject.SetActive(false);
        menuAnimation.movieSeen = true;
    }

}
