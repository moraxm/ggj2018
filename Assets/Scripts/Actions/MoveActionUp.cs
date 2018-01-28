using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionUp : MoveAction
{
    public MoveActionUp()
    {
        dir = MapManager.DIRECTION.TOP;
    }

    public override void preAction(CharController currentPlayer)
    {
      
        m_target = currentPlayer.tablePosition;
        --m_target.y;
        Debug.Log("preAction UP target:" + m_target);
        base.preAction(currentPlayer);
        usePizarrita(m_target);
        currentPlayer.transform.forward = new Vector3(0, 0, 1);
    }


    public override void move(CharController currentPlayer)
    {
        Debug.Log("move UP");
        base.move(currentPlayer);
        
    }
    
}
