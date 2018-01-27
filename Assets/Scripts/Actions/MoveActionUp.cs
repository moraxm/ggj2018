using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionUp : MoveAction
{
    MoveActionUp()
    {
        dir = MapManager.DIRECTION.LEFT;
    }

    public override void preAction(CharController currentPlayer)
    {
        base.preAction(currentPlayer);
        ++m_target.y;
        usePizarrita(m_target);
    }


    public override void move(CharController currentPlayer)
    {
        base.move(currentPlayer);
        currentPlayer.animator.rootPosition = new Vector3(0, 0, 1);
    }
    
}
