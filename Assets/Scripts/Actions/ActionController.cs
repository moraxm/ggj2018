using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {
    public IAction m_R2Action;
    public IAction m_L2Action;
    public MoveAction m_RIGHTAction;
    public MoveAction m_LEFTAction;
    public MoveAction m_UPAction;
    public MoveAction m_DOWNAction;

    public IAction currentAction
    {
        get;
        private set;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
