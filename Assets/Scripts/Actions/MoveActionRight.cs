using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionRight : MoveAction
{
    public MoveActionRight()
    {
        dir = MapManager.DIRECTION.RIGHT;
    }

    public override void preAction(CharController currentPlayer)
    {
        m_target = currentPlayer.tablePosition;
        ++m_target.x;
        Debug.Log("preAction RIGHT target:" + m_target);
        base.preAction(currentPlayer);
        usePizarrita(m_target);
        currentPlayer.transform.forward = new Vector3(1, 0, 0);
    }

    public override void move(CharController currentPlayer)
    {
        base.move(currentPlayer);
        
    }
}
    
