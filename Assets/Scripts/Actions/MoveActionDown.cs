using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActionDown : MoveAction
{
    public MoveActionDown()
    {
        dir = MapManager.DIRECTION.BOTTOM;
    }
    
}
