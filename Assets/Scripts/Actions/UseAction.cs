using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAction : IAction
{
    public override void postAction(CharController currentPlayer)
    {
    }

    public override void preAction(CharController currentPlayer)
    {
    }

    public override void startAction(CharController currentPlayer)
    {
        base.startAction(currentPlayer);
        if (currentPlayer.m_mapManager.doAction(currentPlayer.tablePosition, currentPlayer.orientation, currentPlayer.characterType))
        {
            currentPlayer.animator.SetTrigger("DoMagic");
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
}
