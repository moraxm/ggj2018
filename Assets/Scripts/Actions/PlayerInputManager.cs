using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

// El PlayerInputManager es el una mierda que controla el input de cada
// jugador. Hay uno por cada mando/jugador
public class PlayerInputManager : MonoBehaviour 
{
    public struct actionstruct
    {
        public IAction currentAction;
        public CharController.COLORS currentPLayer;
    }
    private actionstruct m_currentActionStruct;
    public uint m_inputplayerNumber;
    public IAction m_R2Action;
    public IAction m_L2Action;
    public MoveAction m_RIGHTAction;
    public MoveAction m_LEFTAction;
    public MoveAction m_UPAction;
    public MoveAction m_DOWNAction;

    public actionstruct getCurrentActionData()
    {
        return m_currentActionStruct;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Primero las jodidas teclas de colores para saber qué puto personaje se maneja
        m_currentActionStruct.currentPLayer = CharController.COLORS.NONE;
        if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "GREEN"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.GREEN;
        }
        else if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "BLUE"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.BLUE;
        }
        else if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "YELLOW"))
        {
            Debug.Log("Yellowsito");
            m_currentActionStruct.currentPLayer = CharController.COLORS.YELLOW;
        }
        else if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "RED"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.RED;
        }
        if (m_currentActionStruct.currentPLayer == CharController.COLORS.NONE)  return; // No player selected


        // Ahora como hay un personaje selecionado se comprueba la tecla de acción que el jugador quiere hacer
        if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "ActionR"))
        {
            m_currentActionStruct.currentAction = m_R2Action;
        }
        else if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "ActionL"))
        {
            m_currentActionStruct.currentAction = m_L2Action;
        }

        // Ahora como hay un personaje selecionado se comprueban los bumpers
        Debug.Log("Player" + m_inputplayerNumber.ToString() + "VibrateR");
        Debug.Log("Player" + m_inputplayerNumber.ToString() + "VibrateL");
        if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "VibrateR"))
        {
            Debug.Log("VIBRA COÑOL");
            GamePad.SetVibration((PlayerIndex)m_inputplayerNumber, 1.0f, 1.0f);
        }
        if (Input.GetButtonDown("Player" + m_inputplayerNumber.ToString() + "VibrateL"))
        {
            Debug.Log("VIBRA COÑOR");
            GamePad.SetVibration((PlayerIndex)m_inputplayerNumber, 1.0f, 1.0f);
        }

	}
}
