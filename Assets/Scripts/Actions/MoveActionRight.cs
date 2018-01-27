using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionRight : MoveAction
{
    MoveActionRight()
    {
        dir = MapManager.DIRECTION.RIGHT;
    }

    public override void preAction(CharController currentPlayer)
    {
        base.preAction(currentPlayer);
        ++m_target.x;
        usePizarrita(m_target);
    }

    public override void move(CharController currentPlayer)
    {
        base.move(currentPlayer);
        currentPlayer.animator.rootPosition = new Vector3(1, 0, 0);
    }
}
    
