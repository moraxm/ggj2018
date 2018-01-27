using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaleraAction : MapAction
{
    public List<GameObject> players;

    public override void doAction()
    {
        foreach (GameObject go in players)
        {
            Debug.Log("hacer animacion de escalera");
        }
        Debug.Log("ARREGLAME MANU");
    }
}
