using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum DIRECTION  {TOP, BOTTOM, LEFT, RIGHT};
    public Casilla topLeft;
    public int ALTO;
    public int ANCHO;


    Casilla[,] _structure;

    // Use this for initialization
    void Start()
    {
        buildStructure();
    }

    void buildStructure()
    {
        //inicializamos la structura
        _structure = new Casilla[ALTO, ANCHO];
        for (int i = 0; i < ALTO; ++i)
        {
            for (int j = 0; j < ANCHO; ++j)
            {
                _structure[i, j] = null;
            }
        }
        _structure[0,0] = topLeft;

        List<KeyValuePair<int, int>> toAnalyze = new List<KeyValuePair<int, int>>(); toAnalyze.Add(new KeyValuePair<int, int>(0,0));
        List<KeyValuePair<int, int>> allReadyAnalized = new List<KeyValuePair<int, int>>();

        while (toAnalyze.Count > 0)
        {
            //obtengo el primero
            KeyValuePair<int, int> first = toAnalyze[0];
            toAnalyze.RemoveAt(0);
            allReadyAnalized.Add(first);

            Casilla _casilla = _structure[first.Key, first.Value];
            //añadimos todos sus posibles destinos

            List<KeyValuePair<int, int>> futures = new List<KeyValuePair<int, int>>();
            KeyValuePair<int, int> t = new KeyValuePair<int, int>(first.Key - 1, first.Value);
            KeyValuePair<int, int> b = new KeyValuePair<int, int>(first.Key + 1, first.Value);
            KeyValuePair<int, int> l = new KeyValuePair<int, int>(first.Key, first.Value - 1);
            KeyValuePair<int, int> r = new KeyValuePair<int, int>(first.Key, first.Value + 1);

            futures.Add(t); futures.Add(b); futures.Add(l); futures.Add(r);

            for (int futIndex = 0; futIndex < futures.Count; ++futIndex)
            {
                KeyValuePair<int, int> fut = futures[futIndex];

                if (fut.Key < 0 || fut.Value < 0 || fut.Key >= ALTO || fut.Value >= ANCHO)
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

            List<KeyValuePair<Vector3, Vector3>> hits = new List<KeyValuePair<Vector3, Vector3>>();
            hits.Add(new KeyValuePair<Vector3, Vector3>(new Vector3(0, 0, 1), new Vector3(-1, 0, 0)));//top
            hits.Add(new KeyValuePair<Vector3, Vector3>(new Vector3(0, 0, -1), new Vector3(1, 0, 0)));//botom
            hits.Add(new KeyValuePair<Vector3, Vector3>(new Vector3(-1, 0, 0), new Vector3(0, -1, 0)));//left
            hits.Add(new KeyValuePair<Vector3, Vector3>(new Vector3(1, 0, 0), new Vector3(0, 1, 0)));//right

            for (int indexRay = 0; indexRay < hits.Count; ++indexRay)
            {
                KeyValuePair<Vector3, Vector3> rayComponents = hits[indexRay];
                Ray ray = new Ray(miPos, rayComponents.Key);
                Casilla.PERSONAJE_ENUM valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                LayerMask layerCasilla = LayerMask.GetMask("Casilla");
                if (Physics.Raycast(ray, out hitTop, 1, layerCasilla))
                {
                    Casilla casillaColision = hitTop.collider.gameObject.GetComponent<Casilla>();
                    if (_structure[first.Key + (int)rayComponents.Value.x, first.Value + (int)rayComponents.Value.y] == null)
                    {
                        casillaColision.gameObject.name = (first.Key + (int)rayComponents.Value.x).ToString() + "," + (first.Value + (int)rayComponents.Value.y).ToString();
                        //lo añadimos porque en algun momento se usara
                        _structure[first.Key + (int)rayComponents.Value.x, first.Value + (int)rayComponents.Value.y] = casillaColision;
                    }
                    if (!casillaColision._accesible)
                    {
                        valueToSet = Casilla.PERSONAJE_ENUM.NONE;
                    }
                    else
                    {
                        Ray rayCheckPared = new Ray(new Vector3(miPos.x, miPos.y + 0.1f, miPos.z), rayComponents.Key);
                        LayerMask layerPared = LayerMask.GetMask("Pared");
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

    public bool canMove(Vector2Int origin, DIRECTION direction, out Vector3 destinyCoords )
    {
        Vector2 destiny = new Vector2(origin.x, origin.y);
        destinyCoords = Vector3.zero;
        bool canMove = false;
        switch(direction)
        {
            case DIRECTION.TOP:  canMove = _structure[origin.x, origin.y]._goTop != Casilla.PERSONAJE_ENUM.NONE; break;
            case DIRECTION.BOTTOM: canMove = _structure[origin.x, origin.y]._goDown != Casilla.PERSONAJE_ENUM.NONE; ; break;
            case DIRECTION.LEFT: canMove = _structure[origin.x, origin.y]._goLeft != Casilla.PERSONAJE_ENUM.NONE; break;
            case DIRECTION.RIGHT: canMove = _structure[origin.x, origin.y]._goRight != Casilla.PERSONAJE_ENUM.NONE; break;
        }
        if(!canMove)
        {
            return false;
        }
        Vector2Int toAdd = Vector2Int.zero;
        switch (direction)
        {
            case DIRECTION.TOP: toAdd = Vector2Int.down; break;
            case DIRECTION.BOTTOM: toAdd = Vector2Int.up; break;
            case DIRECTION.LEFT: toAdd = Vector2Int.left; break;
            case DIRECTION.RIGHT: toAdd = Vector2Int.right; break;
        }
        Vector2Int finalCoord = origin + toAdd;
        destinyCoords = _structure[finalCoord.x, _structure.y].gameObject.transform.position;
        return true;
    }

    public bool doAction(Vector2Int from, DIRECTION direction, Casilla.PERSONAJE_ENUM color)
    {
        return true;
    }
}
