using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionRight : MoveAction
{
    public override void preAction(CharController currentPlayer)
    {
        m_target = new Vector2(currentPlayer.x, currentPlayer.y);
        ++m_target.x;
        usePizarrita(m_target);
    }
}
