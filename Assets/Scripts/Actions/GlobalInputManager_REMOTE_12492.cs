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
    public uint numberOfPlayers;

    public enum InputState
    {
        RUNNING_ACTION,
        WAITING_INPUT,
    }
    public InputState m_currentState;
    
	// Use this for initialization
	void Start () {
        m_currentState = InputState.WAITING_INPUT;
	}
	
	// Update is called once per frame
	void Update () {
        WaitingInputUpdate();
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
                    if (!BLUEPlayer.running)
                        BLUEPlayer.preAction(a.currentAction);
                    break;
                case CharController.COLORS.GREEN:
                    if (!GREENPlayer.running)
                        GREENPlayer.preAction(a.currentAction);
                    break;
                case CharController.COLORS.YELLOW:
                    if (!YELLOWPlayer.running)
                        YELLOWPlayer.preAction(a.currentAction);
                    break;
                case CharController.COLORS.RED:
                    if (!REDPlayer.running)
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
                    if (!BLUEPlayer.running)
                        BLUEPlayer.setAction(a.currentAction);
                    break;
                case CharController.COLORS.GREEN:
                    if (!GREENPlayer.running)
                        GREENPlayer.setAction(a.currentAction);
                    break;
                case CharController.COLORS.YELLOW:
                    if (!YELLOWPlayer.running)
                        YELLOWPlayer.setAction(a.currentAction);
                    break;
                case CharController.COLORS.RED:
                    if (!REDPlayer.running)
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
                    if (!BLUEPlayer.running) 
                        BLUEPlayer.doAction();
                    break;
                case CharController.COLORS.GREEN:
                    if (!GREENPlayer.running) 
                        GREENPlayer.doAction();
                    break;
                case CharController.COLORS.YELLOW:
                    if (!YELLOWPlayer.running) 
                        YELLOWPlayer.doAction();
                    break;
                case CharController.COLORS.RED:
                    if (!REDPlayer.running)
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
                    if (!BLUEPlayer.running) 
                        BLUEPlayer.postAction();
                    break;
                case CharController.COLORS.GREEN:
                    if (!GREENPlayer.running) 
                        GREENPlayer.postAction();
                    break;
                case CharController.COLORS.YELLOW:
                    if (!YELLOWPlayer.running) 
                        YELLOWPlayer.postAction();
                    break;
                case CharController.COLORS.RED:
                    if (!REDPlayer.running)
                        REDPlayer.postAction();
                    break;
            }
        }
        m_currentState = InputState.RUNNING_ACTION;
    }
}
