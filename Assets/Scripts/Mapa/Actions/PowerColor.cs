using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerColor : MapAction
{

    public GameObject go1;

    public override void doAction(Casilla.PERSONAJE_ENUM color)
    {
        GameManager.Instantiate.actionCorrect();
        go1.transform.GetChild(0).transform.gameObject.SetActive(false);
        go1.transform.GetChild(1).transform.gameObject.SetActive(true);
        switch (color)
        {
            case Casilla.PERSONAJE_ENUM.RED: UtilSound.instance.PlaySound("fire", useFamilySounds: true); break;
        }

    }
}
