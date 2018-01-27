using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MapAction
{
    public Animator _door;
    public GameObject go1;
    public GameObject go2;
    public OpenDoor other;

    public override void doAction()
    {
        go1.SetActive(false);
        go2.SetActive(false);
        other.enabled = false;
        _door.SetTrigger("play");
    }
}
