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
        for (int i = 0; i< numberOfPlayers; ++i)
        {
            var a = players[i].getCurrentActionData();
            a.currentAction.preAction();
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
            a.currentAction.doAction();
        }
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
