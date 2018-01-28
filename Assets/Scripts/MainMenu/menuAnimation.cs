using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class menuAnimation : MonoBehaviour {

    public static bool movieSeen = false;
    public EventSystem eventSystem = null;

    void Start()
    {
        if (!menuAnimation.movieSeen && this.gameObject.GetComponent<VideoPlayer>())
        {
            if (eventSystem)
            {
                eventSystem.enabled = false;
            }
            this.gameObject.GetComponent<VideoPlayer>().Play();
        }
    }

	public void end()
    {
        if (eventSystem)
        {
            eventSystem.enabled = true;
        }
        gameObject.SetActive(false);
        menuAnimation.movieSeen = true;
    }

}
