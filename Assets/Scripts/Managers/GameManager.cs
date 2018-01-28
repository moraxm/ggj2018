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
    public Image timer = null;

    public float missionTime = 60.0f;
    public float timeToRestartMission = 3.0f;
    public float timeToGoToNextLevel = 3.0f;

    private float currentTime = 0.0f;
    private bool missionFailed = false;
    private bool missionSuccess = false;

    public static uint numberOfPlayers = 1; // Static variable with current numberOfPlayers (set by MainMenu selection)

    public int TotalElementsToDesactivate;

	// Use this for initialization
	void Start ()
    {
        UtilSound.instance.StopAllSounds();
        UtilSound.instance.PlaySound("InGame", loop:true,useFamilySounds: true);
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
            Debug.LogError("[GameManager.Start] Error. Timer Image component not found");
            return;
        }

        currentTime = missionTime;
        Vector3 scale = timer.GetComponent<RectTransform>().localScale;
        timer.GetComponent<RectTransform>().localScale = new Vector3(currentTime / missionTime, scale.y, scale.z);

        missionFailed = false;
        missionSuccess = false;

        Transform playersTransform = transform.Find("PlayersCharacters/Players");
        if (!playersTransform)
        {
            Debug.LogError("[GameManager.Start] Error. Players object not found");
            return;
        }

        for (int i = 0; i < numberOfPlayers; ++i)
        {
            playersTransform.GetChild(i).gameObject.SetActive(true);
        }

        ConfigureActions();
	}

    void ConfigureActions()
    {

        Stack<IAction> actions = new Stack<IAction>();
        Stack<MoveAction> moveActions = new Stack<MoveAction>();

        // Create actions
        /*
        actions.Push(new OpenDoorAction());
        actions.Peek().spriteUI = DoorUI;
        actions.Push(new StairAction());
        actions.Peek().spriteUI = LadderUI;
        actions.Push(new UseAction());
        actions.Peek().spriteUI = UseUI;
        moveActions.Push(new MoveActionUp());
        moveActions.Peek().spriteUI = MoveUpUI;
        moveActions.Push(new MoveActionDown());
        moveActions.Peek().spriteUI = MoveDownUI;
        moveActions.Push(new MoveActionRight());
        moveActions.Peek().spriteUI = MoveRightUI;
        moveActions.Push(new MoveActionLeft());
        moveActions.Peek().spriteUI = MoveLeftUI;
        */

        globalInputManager.players[0].m_LEFTAction = new MoveActionLeft();
        globalInputManager.players[0].m_UPAction = new MoveActionUp();
        globalInputManager.players[0].m_RIGHTAction = new MoveActionRight();
        globalInputManager.players[0].m_DOWNAction = new MoveActionDown();

        globalInputManager.players[0].m_L2Action = new UseAction();
        globalInputManager.players[0].m_R2Action = new OpenDoorAction();
        globalInputManager.players[0].m_C2Action = new StairAction();

        /*
        // Assign non move actions
        while (actions.Count > 0)
        {
            List<int> playersPending = new List<int>();
            for (int i = 0; i < numberOfPlayers; ++i)
            {
                playersPending.Add(i);
            }

            while (playersPending.Count > 0)
            {
                if (actions.Count == 0)
                {
                    break;
                }

                IAction action = actions.Pop();
                bool assigned = false;
                while (!assigned)
                {
                    int rand = playersPending[Random.Range(0, playersPending.Count)];
                    if (globalInputManager.players[rand].m_L2Action == null && globalInputManager.players[rand].m_R2Action != null)
                    {
                        globalInputManager.players[rand].m_L2Action = action;
                        playersPending.Remove(rand);
                        assigned = true;
                    }
                    // It does not have a right action
                    else if (globalInputManager.players[rand].m_L2Action != null && globalInputManager.players[rand].m_R2Action == null)
                    {
                        globalInputManager.players[rand].m_R2Action = action;
                        playersPending.Remove(rand);
                        assigned = true;
                    }
                    // Has none. Select random action
                    else if (globalInputManager.players[rand].m_L2Action == null && globalInputManager.players[rand].m_R2Action == null)
                    {
                        int randButton = Random.Range(0, 2);
                        if (randButton == 0)
                        {
                            globalInputManager.players[rand].m_L2Action = action;
                        }
                        else
                        {
                            globalInputManager.players[rand].m_R2Action = action;
                        }
                        playersPending.Remove(rand);
                        assigned = true;
                    }
                }
            }
        }

        // Assign move actions
        while (moveActions.Count > 0)
        {
            List<int> playersPending = new List<int>();
            for (int i = 0; i < numberOfPlayers; ++i)
            {
                playersPending.Add(i);
            }

            while (playersPending.Count > 0)
            {
                if (moveActions.Count == 0)
                {
                    break;
                }
                MoveAction moveAction = moveActions.Pop();
                int rand = playersPending[Random.Range(0, playersPending.Count)];

                if (moveAction is MoveActionUp)
                {
                    globalInputManager.players[rand].m_UPAction = moveAction;
                    playersPending.Remove(rand);
                }
                else if (moveAction is MoveActionDown)
                {
                    globalInputManager.players[rand].m_DOWNAction = moveAction;
                    playersPending.Remove(rand);
                }
                else if (moveAction is MoveActionRight)
                {
                    globalInputManager.players[rand].m_RIGHTAction = moveAction;
                    playersPending.Remove(rand);
                }
                else if (moveAction is MoveActionLeft)
                {
                    globalInputManager.players[rand].m_LEFTAction = moveAction;
                    playersPending.Remove(rand);
                }
                else
                {
                    Debug.LogError("[GameManager.Start] Error. Por algun motivo el MoveAction no es de ningun tipo hijo");
                    return;
                }
            }
        }
        */
        mainInterfaceController.ConfigurePlayersInterface(globalInputManager);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!missionFailed && !missionSuccess)
        {
            currentTime = Mathf.Max(currentTime - Time.deltaTime, 0.0f);
            Vector3 scale = timer.GetComponent<RectTransform>().localScale;
            timer.GetComponent<RectTransform>().localScale = new Vector3(currentTime / missionTime, scale.y, scale.z);
            if (currentTime == 0.0f)
            {
                FailMission();
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            ReturnToMainMenu();
        }
        else if (Input.GetButtonDown("Restart"))
        {
            Restart();
        }
	}

    public void IncreaseTime(float fAmount)
    {
        Debug.Log("Time increased by " + fAmount);
        if (!missionFailed && !missionSuccess)
        {
            Mathf.Clamp(currentTime + fAmount, 0.0f, missionTime);
        }
    }

    void FailMission()
    {
        if (!missionFailed && !missionSuccess)
        {
            UtilSound.instance.StopAllSounds();
            UtilSound.instance.PlaySound("GameOver");
            Debug.Log("Mission failed!");
            missionFailed = true;
            missionFailInterface.SetActive(true);
        }
    }

    void SucceedMission()
    {
        if (!missionFailed && !missionSuccess)
        {
            Debug.Log("Mission completed!");
            missionSuccess = true;
            missionSuccessInterface.SetActive(true);
        }
    }

    public void Restart()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public Transform GetRedCharacter()
    {
        Transform charactersTransform = transform.Find("PlayersCharacters/Characters");
        if (!charactersTransform)
        {
            Debug.LogError("[GameManager.GetRedCharacter] Error. Characters not found");
            return null;
        }

        return charactersTransform.Find("RedCharacter");
    }

    public Transform GetYellowCharacter()
    {
        Transform charactersTransform = transform.Find("PlayersCharacters/Characters");
        if (!charactersTransform)
        {
            Debug.LogError("[GameManager.GetYellowPlayer] Error. Characters not found");
            return null;
        }

        return charactersTransform.Find("YellowCharacter");
    }

    public Transform GetBlueCharacter()
    {
        Transform charactersTransform = transform.Find("PlayersCharacters/Characters");
        if (!charactersTransform)
        {
            Debug.LogError("[GameManager.GetBluePlayer] Error. Characters not found");
            return null;
        }

        return charactersTransform.Find("BlueCharacter");
    }

    public Transform GetGreenCharacter()
    {
        Transform charactersTransform = transform.Find("PlayersCharacters/Characters");
        if (!charactersTransform)
        {
            Debug.LogError("[GameManager.GetGreenPlayer] Error. Characters not found");
            return null;
        }

        return charactersTransform.Find("GreenCharacter");
    }

    public void actionCorrect()
    {
        --TotalElementsToDesactivate;
        if(TotalElementsToDesactivate <= 0)
        {
            SucceedMission();
        }
    }
}
