using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction : IAction
 {
    protected static Dictionary<Vector2, int> pizarrita;
    protected Vector2 m_target;
    protected void usePizarrita(Vector2 target)
    {
        if (pizarrita.ContainsKey(target))
        {
            pizarrita[target] = pizarrita[target] + 1;
        }
    }

    public override void startAction(CharController currentPlayer)
    {
        if (!pizarrita.ContainsKey(m_target))
        {
            throw new System.Exception("No está bien esto");
        }
        if (pizarrita[m_target] > 1)
        { 
            // Más de un imbécil ha dado para moverse al mismo sitio
        }
        else
        {
            // Flow normal
            // Moverse a m_target
        }
    }
    public  override void postAction(CharController currentPlayer)
    {
        pizarrita.Clear();
    }

 }
