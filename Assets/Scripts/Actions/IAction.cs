using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction 
{
    public abstract void preAction(CharController currentPlayer);
    public abstract void doAction(CharController currentPlayer);
    public abstract void postAction(CharController currentPlayer);
}
