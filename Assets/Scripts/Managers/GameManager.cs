using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public MainInterfaceController mainInterfaceController = null;
    public GlobalInputManager globalInputManager = null;
    public GameObject missionFailInterface = null;
    public GameObject missionSuccessInterface = null;
    public Text timer = null;

    public float missionTime = 60.0f;
    public float timeToRestartMission = 3.0f;
    public float timeToGoToNextLevel = 3.0f;

    private float currentTime = 0.0f;
    private bool missionFailed = false;
    private Coroutine failCoroutine = null;
    private bool missionSuccess = false;
    private Coroutine successCoroutine = null;

    public static uint numberOfPlayers = 2; // Static variable with current numberOfPlayers (set by MainMenu selection)

	// Use this for initialization
	void Start ()
    {
        if (!mainInterfaceController)
        {
            Debug.LogError("[GameManager.Start] Error. MainInterfaceController component not found");
            return;
        }

        if (!globalInputManager)
        {
            Debug.LogError("[GameManager.Start] Error. GlobalInputManager component not found");
            return;
        }

        if (!missionFailInterface)
        {
            Debug.LogError("[GameManager.Start] Error. MissionFail object not found");
            return;
        }

        if (!missionSuccessInterface)
        {
            Debug.LogError("[GameManager.Start] Error. MissionSuccess object not found");
            return;
        }

        if (!timer)
        {
            Debug.LogError("[GameManager.Start] Error. Timer text component not found");
            return;
        }

        // Set selected number of players
        mainInterfaceController.ConfigurePlayersInterface(numberOfPlayers);
        globalInputManager.numberOfPlayers = numberOfPlayers;

        currentTime = missionTime;
        timer.text = "Time: " + currentTime.ToString("000");

        missionFailed = false;
        failCoroutine = null;
        missionSuccess = false;
        successCoroutine = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!missionFailed && !missionSuccess)
        {
            currentTime = Mathf.Max(currentTime - Time.deltaTime, 0.0f);
            timer.text = "Time: " + currentTime.ToString("000");
            if (currentTime == 0.0f)
            {
                FailMission();
            }
        }
	}

    void IncreaseTime(float fAmount)
    {
        if (!missionFailed && !missionSuccess)
        {
            Mathf.Clamp(currentTime + fAmount, 0.0f, missionTime);
            timer.text = "Time: " + currentTime.ToString("000");
        }
    }

    void FailMission()
    {
        if (!missionFailed && !missionSuccess)
        {
            Debug.Log("Mission failed!");
            missionFailed = true;
            missionFailInterface.SetActive(true);
            failCoroutine = StartCoroutine("DelayedRestartMission");
        }
    }

    void SucceedMission()
    {
        if (!missionFailed && !missionSuccess)
        {
            Debug.Log("Mission completed!");
            missionSuccess = true;
            missionSuccessInterface.SetActive(true);
            successCoroutine = StartCoroutine("DelayedGoToNextLevel");
        }
    }

    private IEnumerator DelayedRestartMission()
    {
        yield return new WaitForSeconds(timeToRestartMission);
        Restart();
    }

    public void Restart()
    {
        if (failCoroutine != null)
        {
            StopCoroutine(failCoroutine);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator DelayedGoToNextLevel()
    {
        yield return new WaitForSeconds(timeToGoToNextLevel);
        NextLevel();
    }

    public void NextLevel()
    {
        if (successCoroutine != null)
        {
            StopCoroutine(successCoroutine);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
