using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAction : IAction {

    public override void preAction(CharController currentPlayer)
    {
        UtilSound.instance.PlaySound("door", 1.0f, false, true);
    }

    public override void startAction(CharController currentPlayer)
    {
        currentPlayer.m_mapManager.doAction(currentPlayer.tablePosition, currentPlayer.orientation, currentPlayer.characterType);
    }

    public override void postAction(CharController currentPlayer)
    {

    }
}
