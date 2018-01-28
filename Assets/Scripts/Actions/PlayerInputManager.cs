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

    private uint leftPlayer = 0;
    private uint rightPlayer = 0;

    public actionstruct getCurrentActionData()
    {
        return m_currentActionStruct;
    }


	// Use this for initialization
	void Start ()
    {
        leftPlayer = m_inputplayerNumber - 1;
        if (leftPlayer < 1)
        {
            leftPlayer = GameManager.numberOfPlayers;
        }
        --leftPlayer;

        rightPlayer = m_inputplayerNumber + 1;
        if (rightPlayer > GameManager.numberOfPlayers)
        {
            rightPlayer = 1;
        }
        --rightPlayer;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Se comprueban los bumpers
        if (Input.GetButton("Player" + m_inputplayerNumber.ToString() + "VibrateR"))
        {
            GamePad.SetVibration((PlayerIndex)rightPlayer, 1.0f, 0.0f);
        }
        else
        {
            GamePad.SetVibration((PlayerIndex)rightPlayer, 0.0f, 0.0f);
        }

        if (Input.GetButton("Player" + m_inputplayerNumber.ToString() + "VibrateL"))
        {
            GamePad.SetVibration((PlayerIndex)leftPlayer, 0.0f, 1.0f);
        }
        else
        {
            GamePad.SetVibration((PlayerIndex)leftPlayer, 0.0f, 0.0f);
        }

        // Primero las jodidas teclas de colores para saber qué puto personaje se maneja
        m_currentActionStruct.currentPLayer = CharController.COLORS.NONE;
        if (Input.GetButton("Player" + m_inputplayerNumber.ToString() + "GREEN"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.GREEN;
        }
        else if (Input.GetButton("Player" + m_inputplayerNumber.ToString() + "BLUE"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.BLUE;
        }
        else if (Input.GetButton("Player" + m_inputplayerNumber.ToString() + "YELLOW"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.YELLOW;
        }
        else if (Input.GetButton("Player" + m_inputplayerNumber.ToString() + "RED"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.RED;
        }
        if (m_currentActionStruct.currentPLayer == CharController.COLORS.NONE)  return; // No player selected

        m_currentActionStruct.currentAction = null;
        // Ahora como hay un personaje selecionado se comprueba la tecla de acción que el jugador quiere hacer
        if (Input.GetAxis("Player" + m_inputplayerNumber.ToString() + "ActionR") > 0.25f)
        {
            Debug.Log("Realizo accion R");
            m_currentActionStruct.currentAction = m_R2Action;
        }
        else if (Input.GetAxis("Player" + m_inputplayerNumber.ToString() + "ActionL") > 0.25f)
        {
            Debug.Log("Realizo accion L");
            m_currentActionStruct.currentAction = m_L2Action;
        }
        // Y ahora las de movimiento, que estaría bonito
        else if (Input.GetAxis("Player" + m_inputplayerNumber.ToString() + "Horizontal") > 0.25f)
        {
            //Debug.Log("Me muevo a derecha");
            m_currentActionStruct.currentAction = m_RIGHTAction;
        }
        else if (Input.GetAxis("Player" + m_inputplayerNumber.ToString() + "Horizontal") < -0.25f)
        {
            //Debug.Log("Me muevo a izquierda");
            m_currentActionStruct.currentAction = m_LEFTAction;
        }
        else if (Input.GetAxis("Player" + m_inputplayerNumber.ToString() + "Vertical") > 0.25f)
        {
            //Debug.Log("Me muevo a arriba");
            m_currentActionStruct.currentAction = m_UPAction;
        }
        else if (Input.GetAxis("Player" + m_inputplayerNumber.ToString() + "Vertical") < -0.25f)
        {
            //Debug.Log("Me muevo a abajo");
            m_currentActionStruct.currentAction = m_DOWNAction;
        }

	}
}
