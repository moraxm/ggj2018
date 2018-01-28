using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAction : IAction {

    public override void preAction(CharController currentPlayer)
    {
        throw new System.NotImplementedException();
        UtilSound.instance.PlaySound("door", 1.0f, false, true);
    }

    public override void startAction(CharController currentPlayer)
    {
        base.startAction(currentPlayer);
        throw new System.NotImplementedException();
    }

    public override void postAction(CharController currentPlayer)
    {
        throw new System.NotImplementedException();
    }
}
