using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionLeft : MoveAction
{
    public MoveActionLeft()
    {
        dir = MapManager.DIRECTION.LEFT;
    }
}
