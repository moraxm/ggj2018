using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MapAction
{
    public Animator _door;
    public GameObject go1;
    public GameObject go2;

    public override void doAction(Casilla.PERSONAJE_ENUM color)
    {
        go1.SetActive(false);
        go2.SetActive(false);
        _door.SetTrigger("play");
    }
}
