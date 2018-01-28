using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionDown : MoveAction
{
    public MoveActionDown()
    {
        dir = MapManager.DIRECTION.BOTTOM;
    }
    public override void preAction(CharController currentPlayer)
    {
            m_target = currentPlayer.tablePosition;
            ++m_target.y;
            base.preAction(currentPlayer);
            usePizarrita(m_target);
    }

    public override void move(CharController currentPlayer)
    {
        base.move(currentPlayer);
        currentPlayer.animator.rootPosition = new Vector3(0, 0, -1);
    }
    
}
