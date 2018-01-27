using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    public enum DIRECTION { TOP, BOTTOM, LEFT, RIGHT };
    public Casilla topLeft;
    public int ALTO;
    public int ANCHO;
    public Vector3 alturaEscalera = new Vector3(0, 2f, 0);

    private readonly Vector3Int CASILLA_LEFT = new Vector3Int(-1, 0, 0);
    private readonly Vector3Int CASILLA_RIGHT = new Vector3Int(1, 0, 0);
    private readonly Vector3Int CASILLA_TOP = new Vector3Int(0, -1, 0);
    private readonly Vector3Int CASILLA_BOTTOM = new Vector3Int(0, 1, 0);

    private readonly Vector3Int CASILLA_LEFT_RAY = new Vector3Int(-1, 0, 0);
    private readonly Vector3Int CASILLA_RIGHT_RAY = new Vector3Int(1, 0, 0);
    private readonly Vector3Int CASILLA_TOP_RAY = new Vector3Int(0, 0, 1);
    private readonly Vector3Int CASILLA_BOTTOM_RAY = new Vector3Int(0, 0, -1);

    public readonly Vector3 toIncrease = new Vector3(0, 0.6f, 0);

    Casilla[,] _structure;

    // Use this for initialization
    void Start()
    {
        buildStructure();
    }

    void buildStructure()
    {
        //inicializamos la structura
        _structure = new Casilla[ANCHO, ALTO];
        for (int i = 0; i < ANCHO; ++i)
        {
            for (int j = 0; j < ALTO; ++j)
            {
                _structure[i, j] = null;
            }
        }
        _structure[0, 0] = topLeft;

        List<KeyValuePair<int, int>> toAnalyze = new List<KeyValuePair<int, int>>(); toAnalyze.Add(new KeyValuePair<int, int>(0, 0));
        List<KeyValuePair<int, int>> allReadyAnalized = new List<KeyValuePair<int, int>>();

        while (toAnalyze.Count > 0)
        {
            //obtengo el primero
            KeyValuePair<int, int> first = toAnalyze[0];
            toAnalyze.RemoveAt(0);
            allReadyAnalized.Add(first);

            //Debug.Log(first.Key + "," + first.Value);

            Casilla _casilla = _structure[first.Key, first.Value];
            //añadimos todos sus posibles destinos

            List<KeyValuePair<int, int>> futures = new List<KeyValuePair<int, int>>();
            KeyValuePair<int, int> t = new KeyValuePair<int, int>(first.Key + CASILLA_TOP.x, first.Value + CASILLA_TOP.y);
            KeyValuePair<int, int> b = new KeyValuePair<int, int>(first.Key + CASILLA_BOTTOM.x, first.Value + CASILLA_BOTTOM.y);
            KeyValuePair<int, int> l = new KeyValuePair<int, int>(first.Key + CASILLA_LEFT.x, first.Value + CASILLA_LEFT.y);
            KeyValuePair<int, int> r = new KeyValuePair<int, int>(first.Key + CASILLA_RIGHT.x, first.Value + CASILLA_RIGHT.y);

            futures.Add(t); futures.Add(b); futures.Add(l); futures.Add(r);

            for (int futIndex = 0; futIndex < futures.Count; ++futIndex)
            {
                KeyValuePair<int, int> fut = futures[futIndex];

                if (fut.Key < 0 || fut.Value < 0 || fut.Key >= ANCHO || fut.Value >= ALTO)
                {
                    continue;
                }

                if (toAnalyze.Contains(fut))
                {
                    continue;
                }
                if (allReadyAnalized.Contains(fut))
                {
                    continue;
                }

                toAnalyze.Add(fut);
            }

            //lanzamos rayos para comprobar accesibilidad
            RaycastHit hitTop;
            Vector3 miPos = new Vector3(_casilla.gameObject.transform.position.x, _casilla.gameObject.transform.position.y, _casilla.gameObject.transform.position.z);

            List<KeyValuePair<Vector3Int, Vector3Int>> hits = new List<KeyValuePair<Vector3Int, Vector3Int>>();
            hits.Add(new KeyValuePair<Vector3Int, Vector3Int>(CASILLA_TOP, CASILLA_TOP_RAY));//top
            hits.Add(new KeyValuePair<Vector3Int, Vector3Int>(CASILLA_BOTTOM, CASILLA_BOTTOM_RAY));//botom
            hits.Add(new KeyValuePair<Vector3Int, Vector3Int>(CASILLA_LEFT, CASILLA_LEFT_RAY));//left
            hits.Add(new KeyValuePair<Vector3Int, Vector3Int>(CASILLA_RIGHT, CASILLA_RIGHT_RAY));//right

            LayerMask layerCasilla = LayerMask.GetMask("Casilla");
            LayerMask layerPared = LayerMask.GetMask("Pared");

            for (int indexRay = 0; indexRay < hits.Count; ++indexRay)
            {
                KeyValuePair<Vector3Int, Vector3Int> rayComponents = hits[indexRay];
                Ray ray = new Ray(miPos, rayComponents.Value);
                Casilla.PERSONAJE_ENUM valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                if (Physics.Raycast(ray, out hitTop, 1, layerCasilla))
                {
                    Casilla casillaColision = hitTop.collider.gameObject.GetComponent<Casilla>();
                    if (_structure[first.Key + rayComponents.Key.x, first.Value + rayComponents.Key.y] == null)
                    {
                        casillaColision.gameObject.name = (first.Key + (int)rayComponents.Value.x).ToString() + "," + (first.Value + (int)rayComponents.Value.y).ToString();
                        //lo añadimos porque en algun momento se usara
                        _structure[first.Key + rayComponents.Key.x, first.Value + rayComponents.Key.y] = casillaColision;
                    }
                    if (!casillaColision._accesible)
                    {
                        valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                    }
                    else
                    {
                        Ray rayCheckPared = new Ray(miPos + toIncrease, rayComponents.Value);
                        if (Physics.Raycast(rayCheckPared, out hitTop, 1, layerPared))
                        {
                            Casilla pared = hitTop.collider.gameObject.GetComponent<Casilla>();
                            if (!pared._accesible)
                            {
                                valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                            }
                            else
                            {
                                valueToSet = pared._personaje;
                            }
                        }
                        else
                        {
                            valueToSet = casillaColision._personaje;
                        }
                    }
                }

                else
                {
                    //comprobamos si es una escalera con lo que colisiona
                    Ray rayCheckAltura = new Ray(miPos + alturaEscalera, rayComponents.Value);
                    if (Physics.Raycast(rayCheckAltura, out hitTop, 1, layerCasilla))
                    {
                        //como hay colision, se fija el valueToSet a escalera
                        valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                        Casilla caux = hitTop.transform.GetComponent<Casilla>();

                        if (_structure[first.Key + rayComponents.Key.x, first.Value + rayComponents.Key.y] == null)
                        {
                            caux.gameObject.name = "Escalera " + (first.Key + (int)rayComponents.Value.x).ToString() + "," + (first.Value + (int)rayComponents.Value.y).ToString();
                            //lo añadimos porque en algun momento se usara
                            _structure[first.Key + rayComponents.Key.x, first.Value + rayComponents.Key.y] = caux;
                        }

                        //comprobamos si hay una escalera, si la hay significa que somos escalera
                        Ray rayCheckEscalera = new Ray(miPos + toIncrease, rayComponents.Value);
                        if (Physics.Raycast(rayCheckAltura, out hitTop, 1, layerPared))
                        {
                            if (hitTop.transform.GetComponent<Escalera>() != null)
                            {
                                valueToSet = Casilla.PERSONAJE_ENUM.ESCALERA;
                            }
                        }
                    }

                    rayCheckAltura = new Ray(miPos - alturaEscalera, rayComponents.Value);
                    if (Physics.Raycast(rayCheckAltura, out hitTop, 1, layerCasilla))
                    {
                        //como hay colision, se fija el valueToSet a escalera
                        valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                        Casilla caux = hitTop.transform.GetComponent<Casilla>();

                        if (_structure[first.Key + rayComponents.Key.x, first.Value + rayComponents.Key.y] == null)
                        {
                            caux.gameObject.name = "Escalera " + (first.Key + (int)rayComponents.Value.x).ToString() + "," + (first.Value + (int)rayComponents.Value.y).ToString();
                            //lo añadimos porque en algun momento se usara
                            _structure[first.Key + rayComponents.Key.x, first.Value + rayComponents.Key.y] = caux;
                        }

                        //comprobamos si hay una escalera, si la hay significa que somos escalera
                        Ray rayCheckEscalera = new Ray(miPos - alturaEscalera + toIncrease, rayComponents.Value);
                        if (Physics.Raycast(rayCheckAltura, out hitTop, 1, layerPared))
                        {
                            if (hitTop.transform.GetComponent<Escalera>() != null)
                            {
                                valueToSet = Casilla.PERSONAJE_ENUM.ESCALERA;
                            }
                        }
                    }
                }

                switch (indexRay)
                {
                    case 0: _casilla._goTop = valueToSet; break;
                    case 1: _casilla._goDown = valueToSet; break;
                    case 2: _casilla._goLeft = valueToSet; break;
                    case 3: _casilla._goRight = valueToSet; break;
                }
            }
        }
    }

    public bool canMove(Vector2Int from, DIRECTION direction, out Vector3 destinyCoords)
    {
        Vector2 destiny = new Vector2(from.x, from.y);
        destinyCoords = Vector3.zero;
        bool canMove = false;
        switch (direction)
        {
            case DIRECTION.TOP: canMove = _structure[from.x, from.y]._goTop == Casilla.PERSONAJE_ENUM.NONE; break;
            case DIRECTION.BOTTOM: canMove = _structure[from.x, from.y]._goDown == Casilla.PERSONAJE_ENUM.NONE; ; break;
            case DIRECTION.LEFT: canMove = _structure[from.x, from.y]._goLeft == Casilla.PERSONAJE_ENUM.NONE; break;
            case DIRECTION.RIGHT: canMove = _structure[from.x, from.y]._goRight == Casilla.PERSONAJE_ENUM.NONE; break;
        }
        if (!canMove)
        {
            return false;
        }
        Vector3Int toAdd = Vector3Int.zero;
        switch (direction)
        {
            case DIRECTION.TOP: toAdd = CASILLA_TOP; break;
            case DIRECTION.BOTTOM: toAdd = CASILLA_BOTTOM; break;
            case DIRECTION.LEFT: toAdd = CASILLA_LEFT; break;
            case DIRECTION.RIGHT: toAdd = CASILLA_RIGHT; break;
        }
        Vector2Int finalCoord = from + new Vector2Int(toAdd.x, toAdd.y);
        destinyCoords = _structure[finalCoord.x, finalCoord.y].gameObject.transform.position;
        return true;
    }

    public bool doAction(Vector2Int from, DIRECTION direction, Casilla.PERSONAJE_ENUM color)
    {
        Vector2Int destiny = new Vector2Int(from.x, from.y);
        Casilla.PERSONAJE_ENUM posibility = Casilla.PERSONAJE_ENUM.NONE;

        switch (direction)
        {
            case DIRECTION.TOP: posibility = _structure[from.x, from.y]._goTop; break;
            case DIRECTION.BOTTOM: posibility = _structure[from.x, from.y]._goDown; break;
            case DIRECTION.LEFT: posibility = _structure[from.x, from.y]._goLeft; break;
            case DIRECTION.RIGHT: posibility = _structure[from.x, from.y]._goRight; break;
        }

        if (posibility == Casilla.PERSONAJE_ENUM.ANY)
        {
            return true;
        }

        if (posibility == Casilla.PERSONAJE_ENUM.NONE || posibility == Casilla.PERSONAJE_ENUM.UNKNOW)
        {
            return false;
        }

        //se trata de una accion con color
        if (posibility != color)
        {
            return false;
        }

        //quitar y actualizar las casilas

        Vector3Int toAdd = Vector3Int.zero;
        switch (direction)
        {
            case DIRECTION.TOP: toAdd = CASILLA_TOP; break;
            case DIRECTION.BOTTOM: toAdd = CASILLA_BOTTOM; break;
            case DIRECTION.LEFT: toAdd = CASILLA_LEFT; break;
            case DIRECTION.RIGHT: toAdd = CASILLA_RIGHT; break;
        }
        destiny = from + new Vector2Int(toAdd.x, toAdd.y);

        // si es una casilla de solo un personaje, se actualizan todos los vecinos
        if (_structure[destiny.x, destiny.y]._personaje == posibility)
        {
            //el que esta arriba
            Vector2Int localPostion = destiny + new Vector2Int(CASILLA_TOP.x, CASILLA_TOP.y);
            if (localPostion.x >= 0 && localPostion.x >= 0 && localPostion.y < ANCHO && localPostion.y < ALTO)
            {
                _structure[localPostion.x, localPostion.y]._goDown = Casilla.PERSONAJE_ENUM.ANY;
            }

            //abajo
            localPostion = destiny + new Vector2Int(CASILLA_BOTTOM.x, CASILLA_BOTTOM.y);
            if (localPostion.x >= 0 && localPostion.x >= 0 && localPostion.y < ANCHO && localPostion.y < ALTO)
            {
                _structure[localPostion.x, localPostion.y]._goTop = Casilla.PERSONAJE_ENUM.ANY;
            }

            //derecha
            localPostion = destiny + new Vector2Int(CASILLA_RIGHT.x, CASILLA_RIGHT.y);
            if (localPostion.x >= 0 && localPostion.x >= 0 && localPostion.y < ANCHO && localPostion.y < ALTO)
            {
                _structure[localPostion.x, localPostion.y]._goLeft = Casilla.PERSONAJE_ENUM.ANY;
            }

            //izquierda
            localPostion = destiny + new Vector2Int(CASILLA_LEFT.x, CASILLA_LEFT.y);
            if (localPostion.x >= 0 && localPostion.x >= 0 && localPostion.y < ANCHO && localPostion.y < ALTO)
            {
                _structure[localPostion.x, localPostion.y]._goRight = Casilla.PERSONAJE_ENUM.ANY;
            }
        }
        else
        {
            //son puertas, solo actualizamos destino y actual
            switch (direction)
            {
                case DIRECTION.TOP:
                    _structure[from.x, from.y]._goTop = Casilla.PERSONAJE_ENUM.ANY;
                    _structure[destiny.x, destiny.y]._goDown = Casilla.PERSONAJE_ENUM.ANY;
                    break;
                case DIRECTION.BOTTOM:
                    _structure[from.x, from.y]._goDown = Casilla.PERSONAJE_ENUM.ANY;
                    _structure[destiny.x, destiny.y]._goTop = Casilla.PERSONAJE_ENUM.ANY;
                    break;

                case DIRECTION.LEFT:
                    _structure[from.x, from.y]._goLeft = Casilla.PERSONAJE_ENUM.ANY;
                    _structure[destiny.x, destiny.y]._goRight = Casilla.PERSONAJE_ENUM.ANY;
                    break;

                case DIRECTION.RIGHT:
                    _structure[from.x, from.y]._goRight = Casilla.PERSONAJE_ENUM.ANY;
                    _structure[destiny.x, destiny.y]._goLeft = Casilla.PERSONAJE_ENUM.ANY;
                    break;
            }
        }

        return true;
    }
}
