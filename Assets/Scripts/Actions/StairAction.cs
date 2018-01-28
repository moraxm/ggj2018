using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairAction : IAction
{

    public StairAction()
    {
        actionDuration = 4;
    }
    public override void preAction(CharController currentPlayer)
    {

    }

    public override void startAction(CharController currentPlayer)
    {
        base.startAction(currentPlayer);
        string trigger = "";
        bool ok = false;
        if (currentPlayer.m_mapManager.doAction(currentPlayer.tablePosition, currentPlayer.orientation, Casilla.PERSONAJE_ENUM.ESCALERA_TOP))
        {
            trigger = "UpStairs";
            ok = true;
        }
        else if (currentPlayer.m_mapManager.doAction(currentPlayer.tablePosition, currentPlayer.orientation, Casilla.PERSONAJE_ENUM.ESCALERA_BOTTOM))
        {
            trigger = "DownStairs";
            ok = true;
        }
        if (ok)
        {
            currentPlayer.animator.SetTrigger(trigger);
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
        else
        {
            currentPlayer.animator.SetTrigger("NoPuedo");
            UtilSound.instance.PlaySound("nono", 1.0f, false, true);
        }

    }

    public override void postAction(CharController currentPlayer)
    {

    }
}
