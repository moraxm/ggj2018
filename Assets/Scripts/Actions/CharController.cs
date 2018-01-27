using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// El PlayerController es una basura que se pone a cada personaje
// para poder moverlo y tal y se le enchufa una acción para que la ejecute
public class CharController : MonoBehaviour
{
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

    MapManager.DIRECTION orientation
    {
        get;
        set;
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
    public enum COLORS
    {
        GREEN,
        RED,
        BLUE,
        YELLOW,
        NONE,
    }
    public void setAction(IAction action)
    {
        if (m_action != null)
        {
            int a = Random.Range(0, 1);
            if (a > 0)
                m_action = action;
        }
        else
        {
            m_action = action;
        }
    }

    public void doAction()
    {
        m_action.startAction(this);
    }

    public void preAction(IAction action)
    {
        action.preAction(this);
    }

    public void postAction()
    {
        m_action.postAction(this);
    }
}
