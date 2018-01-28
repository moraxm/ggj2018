using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

    public ScrollRect scrollRect = null;
    public float fSpeed = 0.5f;
    public float fTimeToReturnToMainMenu = 5.0f;

    private Coroutine closeCoroutine = null;

	void Awake ()
    {
        closeCoroutine = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (scrollRect)
        {
            if (scrollRect.verticalNormalizedPosition > 0.01f)
            {
                scrollRect.verticalNormalizedPosition = Mathf.Max(scrollRect.verticalNormalizedPosition - fSpeed * Time.deltaTime, 0.0f);
            }
            else if (closeCoroutine == null)
            {
                closeCoroutine = StartCoroutine("DelayedReturnToMainMenu");
            }
        }
	}

    IEnumerator DelayedReturnToMainMenu()
    {
        yield return new WaitForSeconds(fTimeToReturnToMainMenu);
        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
