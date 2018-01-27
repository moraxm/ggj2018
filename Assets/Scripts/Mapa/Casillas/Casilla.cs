using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    public enum PERSONAJE_ENUM
    {
        ANY, RED, BLUE, GREEN, YELLOW, NONE, UNKNOW, ESCALERA
    }
    /// <summary>
    /// Nos indica si esta casilla es accesible en general, una casilla no accesible tiene por ejemplo un barril encima
    /// </summary>
    public bool _accesible;

    /// <summary>
    /// indica que personajes pueden estar en esta posicion
    /// </summary>
    public PERSONAJE_ENUM _personaje;

    /// <summary>
    /// indica que personajes pueden ir en esta direccion
    /// </summary>
    public PERSONAJE_ENUM _goLeft;
    /// <summary>
    /// indica que personajes pueden ir en esta direccion
    /// </summary>
    public PERSONAJE_ENUM _goRight;
    
    /// <summary>
    /// indica que personajes pueden ir en esta direccion
    /// </summary>
    public PERSONAJE_ENUM _goTop;
    
    /// <summary>
    /// indica que personajes pueden ir en esta direccion
    /// </summary>
    public PERSONAJE_ENUM _goDown;
}
