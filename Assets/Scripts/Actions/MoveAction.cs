using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction : IAction
 {
    public MapManager.DIRECTION dir;
    protected static Dictionary<Vector2Int, int> pizarrita;
    protected Vector3 m_currentPosition;
    protected Vector3 m_targetPosition;
    protected Vector2Int m_target;
    protected void usePizarrita(Vector2Int target)
    {
        if (pizarrita.ContainsKey(target))
        {
            pizarrita[target] = pizarrita[target] + 1;
        }
    }

    public override void preAction(CharController currentPlayer)
    {
        //m_target = new Vector2Int(currentPlayer.x, currentPlayer.y);
        if (!currentPlayer.m_mapManager.canMove(m_target, dir, out m_targetPosition))
        {
            usePizarrita(m_target);// Se hace doble pizarrita para que parezca como que hay dos personas intentando
            // ir al mismo sitio y no se pueda.
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
            // animación de orientación
        }
        else
        {
            // Flow normal
            // Moverse a m_target
            move(currentPlayer);
        }
    }
    public  override void postAction(CharController currentPlayer)
    {
        pizarrita.Clear();
    }

    virtual public void move(CharController currentPlayer)
    {
        currentPlayer.animator.SetTrigger("Forward");
    }

 }
