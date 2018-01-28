using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerColor : MapAction
{

    public GameObject go1;

    public override void doAction(Casilla.PERSONAJE_ENUM color)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().actionCorrect();
        go1.transform.GetChild(0).transform.gameObject.SetActive(false);
        go1.transform.GetChild(1).transform.gameObject.SetActive(true);
        switch (color)
        {
            case Casilla.PERSONAJE_ENUM.RED: UtilSound.instance.PlaySound("extintor", useFamilySounds: true); break;
            case Casilla.PERSONAJE_ENUM.YELLOW: UtilSound.instance.PlaySound("cables", useFamilySounds: true); break;
            case Casilla.PERSONAJE_ENUM.BLUE: UtilSound.instance.PlaySound("reparatuberia", useFamilySounds: true); break;
            case Casilla.PERSONAJE_ENUM.GREEN: UtilSound.instance.PlaySound("closegas", useFamilySounds: true); break;
        }

    }
}
