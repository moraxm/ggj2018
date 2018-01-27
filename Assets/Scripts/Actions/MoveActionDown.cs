using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionDown : MoveAction {
    public override void preAction(CharController currentPlayer)
    {
        m_target = new Vector2(currentPlayer.x,currentPlayer.y);
        --m_target.y;
        usePizarrita(m_target);
    }
}
