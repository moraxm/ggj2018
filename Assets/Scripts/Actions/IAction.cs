using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction 
{
    void preAction(CharController currentPlayer);
    void doAction(CharController currentPlayer);
    void postAction(CharController currentPlayer);
}
