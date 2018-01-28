using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionLeft : MoveAction
{
    public MoveActionLeft()
    {
        dir = MapManager.DIRECTION.LEFT;
    }
    public override void preAction(CharController currentPlayer)
    {
            m_target = currentPlayer.tablePosition;
            --m_target.x;
            base.preAction(currentPlayer);
            usePizarrita(m_target);
    }

    public override void move(CharController currentPlayer)
    {
        base.move(currentPlayer);
        currentPlayer.animator.rootPosition = new Vector3(-1, 0, 0);
    }
}
