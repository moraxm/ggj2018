using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Sprite configuration for each action
    public Sprite MoveUpUI = null;
    public Sprite MoveDownUI = null;
    public Sprite MoveLeftUI = null;
    public Sprite MoveRightUI = null;
    public Sprite LadderUI = null;
    public Sprite DoorUI = null;
    public Sprite UseUI = null;

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

    public static uint numberOfPlayers = 3; // Static variable with current numberOfPlayers (set by MainMenu selection)

	// Use this for initialization
	void Start ()
    {
        if (!MoveUpUI || !MoveDownUI || !MoveLeftUI || !MoveRightUI || !LadderUI || !DoorUI || !UseUI)
        {
            Debug.LogError("[MainInterfaceController.Start] Error. An action sprite has not been configured via editor");
            return;
        }

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

        currentTime = missionTime;
        timer.text = "Time: " + currentTime.ToString("000");

        missionFailed = false;
        failCoroutine = null;
        missionSuccess = false;
        successCoroutine = null;

        ConfigureActions();
	}

    void ConfigureActions()
    {

        Stack<IAction> actions = new Stack<IAction>();
        Stack<MoveAction> moveActions = new Stack<MoveAction>();

        // Create actions
        actions.Push(new OpenDoorAction());
        actions.Peek().spriteUI = DoorUI;
        actions.Push(new OpenDoorAction());
        actions.Peek().spriteUI = LadderUI;
        actions.Push(new OpenDoorAction());
        actions.Peek().spriteUI = UseUI;
        moveActions.Push(new MoveActionUp());
        moveActions.Peek().spriteUI = MoveUpUI;
        moveActions.Push(new MoveActionDown());
        moveActions.Peek().spriteUI = MoveDownUI;
        moveActions.Push(new MoveActionRight());
        moveActions.Peek().spriteUI = MoveRightUI;
        moveActions.Push(new MoveActionLeft());
        moveActions.Peek().spriteUI = MoveLeftUI;

        // Non move actions
        while (actions.Count > 0)
        {
            // Select random player until we get one with a missing action
            int randPlayer = 0;
            do
            {
                randPlayer = Random.Range(0, (int)numberOfPlayers);
            }
            while (globalInputManager.players[randPlayer].m_L2Action != null && globalInputManager.players[randPlayer].m_R2Action != null);
            // It does not have a left action
            if (globalInputManager.players[randPlayer].m_L2Action == null && globalInputManager.players[randPlayer].m_R2Action != null)
            {
                globalInputManager.players[randPlayer].m_L2Action = actions.Pop();
            }
            // It does not have a right action
            else if (globalInputManager.players[randPlayer].m_L2Action != null && globalInputManager.players[randPlayer].m_R2Action == null)
            {
                globalInputManager.players[randPlayer].m_R2Action = actions.Pop();
            }
            else // Has none. Select random action
            {
                int randButton = Random.Range(0, 2);
                if (randButton == 0)
                {
                    globalInputManager.players[randPlayer].m_L2Action = actions.Pop();
                }
                else
                {
                    globalInputManager.players[randPlayer].m_R2Action = actions.Pop();
                }
            }
        }

        // Move actions
        // Assign one MoveAction to each player
        for (uint i = 0; i < numberOfPlayers; ++i)
        {
            MoveAction moveAction = moveActions.Pop();
            if (moveAction is MoveActionUp)
            {
                globalInputManager.players[i].m_UPAction = moveAction;
            }
            else if (moveAction is MoveActionDown)
            {
                globalInputManager.players[i].m_DOWNAction = moveAction;
            }
            else if (moveAction is MoveActionRight)
            {
                globalInputManager.players[i].m_RIGHTAction = moveAction;
            }
            else if (moveAction is MoveActionLeft)
            {
                globalInputManager.players[i].m_LEFTAction = moveAction;
            }
        }

        while (moveActions.Count > 0)
        {
            MoveAction moveAction = moveActions.Pop();

            while (true)
            {
                int rand = Random.Range(0, (int)numberOfPlayers);
                if (moveAction is MoveActionUp)
                {
                    if (globalInputManager.players[rand].m_UPAction == null)
                    {
                        globalInputManager.players[rand].m_UPAction = moveAction;
                        break;
                    }
                }
                else if (moveAction is MoveActionDown)
                {
                    if (globalInputManager.players[rand].m_DOWNAction == null)
                    {
                        globalInputManager.players[rand].m_DOWNAction = moveAction;
                        break;
                    }
                }
                else if (moveAction is MoveActionRight)
                {
                    if (globalInputManager.players[rand].m_RIGHTAction == null)
                    {
                        globalInputManager.players[rand].m_RIGHTAction = moveAction;
                        break;
                    }
                }
                else if (moveAction is MoveActionLeft)
                {
                    if (globalInputManager.players[rand].m_LEFTAction == null)
                    {
                        globalInputManager.players[rand].m_LEFTAction = moveAction;
                        break;
                    }
                }
            }
        }

        mainInterfaceController.ConfigurePlayersInterface(globalInputManager);
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
