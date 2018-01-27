using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour 
{
    public struct actionstruct
    {
        public IAction currentAction;
        public Players.COLORS currentPLayer;
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
        return m_
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Primero las jodidas teclas de colores para saber qué puto personaje se maneja
        Players.COLORS currentPLayerSelected = Players.COLORS.NONE;
		if (Input.GetButtonDown(m_inputplayerName+"GREEN"))
        {
            currentPLayerSelected = Players.COLORS.GREEN;
        }
        else if (Input.GetButtonDown(m_inputplayerName+"BLUE"))
        {
            currentPLayerSelected = Players.COLORS.BLUE;
        }
        else if (Input.GetButtonDown(m_inputplayerName+"YELLOW"))
        {
            currentPLayerSelected = Players.COLORS.YELLOW;
        }
        else if (Input.GetButtonDown(m_inputplayerName+"RED"))
        {
            currentPLayerSelected = Players.COLORS.RED;
        }
        if (currentPLayerSelected == Players.COLORS.NONE) return; // No player selected


	}
}
