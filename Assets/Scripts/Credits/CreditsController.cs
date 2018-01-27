using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {

    public ScrollRect scrollRect = null;
    public float fSpeed = 0.5f;
    public float fTimeToReturnToMainMenu = 5.0f;

    private bool bAlreadyQuitting = false;

	void Awake ()
    {
        bAlreadyQuitting = false;
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
            else if (!bAlreadyQuitting)
            {
                StartCoroutine("ReturnToMainMenu");
                bAlreadyQuitting = true;
            }
        }
	}

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(fTimeToReturnToMainMenu);
        SceneManager.LoadScene("MainMenu");
    }
}
