using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerColor : MapAction {

    public GameObject graphic;
    public GameObject _animation;

     public override void doAction()
    {
        if(graphic != null)
        {
            graphic.SetActive(false);
        }

        if (_animation != null)
        {
            _animation.SetActive(false);
        }
    }
}
