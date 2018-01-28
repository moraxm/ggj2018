using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// El PlayerController es una basura que se pone a cada personaje
// para poder moverlo y tal y se le enchufa una acción para que la ejecute
public class CharController : MonoBehaviour
{
    public Casilla.PERSONAJE_ENUM characterType;
    public Vector2Int tablePosition;
    public Animator animator
    {
        get;
        private set;
    }
    public void Start()
    {
        if (!m_mapManager)
            m_mapManager = FindObjectOfType<MapManager>();
        animator = GetComponent<Animator>();
    }

    public MapManager.DIRECTION orientation
    {
        get;
        set;
    }
    public enum COLORS
    {
        GREEN,
        RED,
        BLUE,
        YELLOW,
        NONE,
    }

    [HideInInspector]
    public MapManager m_mapManager;
    public bool running
    {
        get
        {
            if (m_action != null) return m_action.running;
            else return false;
        }
    }
    IAction m_action;
    public void setAction(IAction action)
    {
        if (m_action != null && m_action.running) return;
        //if (m_action != null)
        //{
        //    int a = Random.Range(0, 2);
        //    if (a > 0)
        //        m_action = action;
        //}
        //else
        {
            m_action = action;
        }
    }

    public void doAction()
    {
        if (m_action != null)
        {
            m_action.startAction(this);
        }
    }

    public void preAction(IAction action)
    {
        action.preAction(this);
    }

    public void postAction()
    {
        if (m_action != null && !m_action.running)
        {
            m_action.postAction(this);
            m_action = null;
        }
    }

    void Update()
    {
        if (m_action != null && m_action.running)
            m_action.updateAction(this);
    }
}
