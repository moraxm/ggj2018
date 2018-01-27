using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// El GlobalInputManager gestiona el input de todos los PlayerInputManagers
// para que no conflicteen acciones entre sí y para randomizar quién da primero
// a los botones, vamos una basura.
public class GlobalInputManager : MonoBehaviour {

    public CharController REDPlayer;
    public CharController BLUEPlayer;
    public CharController GREENPlayer;
    public CharController YELLOWPlayer;

    public PlayerInputManager[] players;
    public int numberOfPlayers;

    public enum InputState
    {
        RUNNING_ACTION,
        WAITING_INPUT,
    }
    public InputState m_currentState;
    public float m_ActionsTime;
    private float m_acumTime;
	// Use this for initialization
	void Start () {
        m_currentState = InputState.WAITING_INPUT;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_currentState)
        {
            case InputState.RUNNING_ACTION:
                RunningActionUpdate();
                break;
            case InputState.WAITING_INPUT:
                WaitingInputUpdate();
                break;
        }
	}

    private void WaitingInputUpdate()
    {
        // Primero el pre
        bool someAction = false;
        for (int i = 0; i< numberOfPlayers; ++i)
        {
            var a = players[i].getCurrentActionData();
            someAction = someAction || a.currentAction != null;
            switch (a.currentPLayer)
            {
                case CharController.COLORS.BLUE:
                    BLUEPlayer.preAction(a.currentAction);
                    break;
                case CharController.COLORS.GREEN:
                    GREENPlayer.preAction(a.currentAction);
                    break;
                case CharController.COLORS.YELLOW:
                    YELLOWPlayer.preAction(a.currentAction);
                    break;
                case CharController.COLORS.RED:
                    REDPlayer.preAction(a.currentAction);
                    break;
            }    
        }

        if (!someAction)
        {
            m_currentState = InputState.WAITING_INPUT;
            return;
        }
        
        // Un bucle más, ascazo
        for (int i = 0; i< numberOfPlayers; ++i)
        {
            var a = players[i].getCurrentActionData();
            switch (a.currentPLayer) 
            { 
                case CharController.COLORS.BLUE:
                    BLUEPlayer.setAction(a.currentAction);
                    break;
                case CharController.COLORS.GREEN:
                    GREENPlayer.setAction(a.currentAction);
                    break;
                case CharController.COLORS.YELLOW:
                    YELLOWPlayer.setAction(a.currentAction);
                    break;
                case CharController.COLORS.RED:
                    REDPlayer.setAction(a.currentAction);
                    break;
            }     
        }

        // El tercer bucle, poto
        for (int i = 0; i < numberOfPlayers; ++i)
        {
            var a = players[i].getCurrentActionData();
            switch (a.currentPLayer)
            {
                case CharController.COLORS.BLUE:
                    BLUEPlayer.doAction();
                    break;
                case CharController.COLORS.GREEN:
                    GREENPlayer.doAction();
                    break;
                case CharController.COLORS.YELLOW:
                    YELLOWPlayer.doAction();
                    break;
                case CharController.COLORS.RED:
                    REDPlayer.doAction();
                    break;
            }
        }

        // El cuarto bucle, quiero morir
        for (int i = 0; i < numberOfPlayers; ++i)
        {
            var a = players[i].getCurrentActionData();
            switch (a.currentPLayer)
            {
                case CharController.COLORS.BLUE:
                    BLUEPlayer.postAction();
                    break;
                case CharController.COLORS.GREEN:
                    GREENPlayer.postAction();
                    break;
                case CharController.COLORS.YELLOW:
                    YELLOWPlayer.postAction();
                    break;
                case CharController.COLORS.RED:
                    REDPlayer.postAction();
                    break;
            }
        }

        m_currentState = InputState.RUNNING_ACTION;
    }

    private void RunningActionUpdate()
    {
        // Espera hasta que termina la jodida acción
        m_acumTime += Time.deltaTime;
        if (m_acumTime > m_ActionsTime)
        {
            m_acumTime = 0;
            m_currentState = InputState.WAITING_INPUT;
        }
    }
}
