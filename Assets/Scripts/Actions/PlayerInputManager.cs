using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string m_inputplayerName;
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
		if (Input.GetButtonDown(m_inputplayerName+"GREEN"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.GREEN;
        }
        else if (Input.GetButtonDown(m_inputplayerName+"BLUE"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.BLUE;
        }
        else if (Input.GetButtonDown(m_inputplayerName+"YELLOW"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.YELLOW;
        }
        else if (Input.GetButtonDown(m_inputplayerName+"RED"))
        {
            m_currentActionStruct.currentPLayer = CharController.COLORS.RED;
        }
        if (m_currentActionStruct.currentPLayer == CharController.COLORS.NONE)  return; // No player selected




        // Ahora como hay un personaje selecionado se comprueba le tecla de acción que el jugador quiere hacer
        if (Input.GetButtonDown(m_inputplayerName + "ActionR"))
        {
            m_currentActionStruct.currentAction = m_R2Action;
        }
        else if (Input.GetButtonDown(m_inputplayerName + "ActionL"))
        {
            m_currentActionStruct.currentAction = m_L2Action;
        }

	}
}
