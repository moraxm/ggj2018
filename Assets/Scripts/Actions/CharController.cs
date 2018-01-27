using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// El PlayerController es una basura que se pone a cada personaje
// para poder moverlo y tal y se le enchufa una acción para que la ejecute
public class CharController : MonoBehaviour
{
    public int x;
    public int y;
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
        if(m_action != null)
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
        m_action.doAction(this);
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
