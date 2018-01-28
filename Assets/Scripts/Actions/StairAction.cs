using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairAction : IAction
{

    public StairAction()
    {
        actionDuration = 3;
    }
    public override void preAction(CharController currentPlayer)
    {

    }

    public override void startAction(CharController currentPlayer)
    {
        base.startAction(currentPlayer);
        Debug.Log("Trigger Stairs");
        if (currentPlayer.m_mapManager.doAction(currentPlayer.tablePosition, currentPlayer.orientation, Casilla.PERSONAJE_ENUM.ESCALERA))
        {
            currentPlayer.animator.SetTrigger("UpStairs");
            UtilSound.instance.PlaySound("ladder", 1.0f, false, true);
            Vector2Int pos = currentPlayer.tablePosition;
            switch (currentPlayer.orientation)
            {
                case MapManager.DIRECTION.RIGHT:
                    ++pos.x;
                    break;
                case MapManager.DIRECTION.LEFT:
                    --pos.x;
                    break;
                case MapManager.DIRECTION.BOTTOM:
                    ++pos.y;
                    break;
                case MapManager.DIRECTION.TOP:
                    --pos.y;
                    break;
            }
            currentPlayer.tablePosition = pos;
        }

    }

    public override void postAction(CharController currentPlayer)
    {

    }
}
