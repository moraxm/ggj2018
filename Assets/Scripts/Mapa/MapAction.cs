using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapAction : MonoBehaviour {

    public abstract void doAction(Casilla.PERSONAJE_ENUM color) ;
}
