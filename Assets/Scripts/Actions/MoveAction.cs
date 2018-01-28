using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveAction : IAction
 {
    public MapManager.DIRECTION dir;
    public static Dictionary<Vector2Int, int> pizarrita = new Dictionary<Vector2Int, int>();
    protected Vector3 m_currentPosition;
    protected Vector3 m_targetPosition;
    protected Vector2Int m_target;
    Vector2Int aux;

    public override void preAction(CharController currentPlayer)
    {
        
    }

    protected bool canMove(CharController currentPlayer)
    {
        return currentPlayer.m_mapManager.canMove(currentPlayer.tablePosition, dir, out m_targetPosition);
    }

    public override void startAction(CharController currentPlayer)
    {
        currentPlayer.orientation = dir;
        rotate(currentPlayer);
        base.startAction(currentPlayer);
        if (!canMove(currentPlayer))
        {
            // Más de un imbécil ha dado para moverse al mismo sitio
            // animación de orientación
            Debug.Log("NO puedo");
            currentPlayer.animator.SetTrigger("NoPuedo");
            UtilSound.instance.PlaySound("nono", 1.0f, false, true);
        }
        else
        {
            // Flow normal
            // Moverse a m_target
            Debug.Log("Move");
            move(currentPlayer);
            UtilSound.instance.PlaySound("walk", 1.0f, false, true);
        }
    }
    public  override void postAction(CharController currentPlayer)
    {
        currentPlayer.tablePosition = aux;

    }

    public void rotate(CharController currentPlayer)
    {
        switch (currentPlayer.orientation)
        {
            case MapManager.DIRECTION.RIGHT:
                currentPlayer.transform.forward = new Vector3(1, 0, 0);
                break;
            case MapManager.DIRECTION.LEFT:
                currentPlayer.transform.forward = new Vector3(-1, 0, 0);
                break;
            case MapManager.DIRECTION.BOTTOM:
                currentPlayer.transform.forward = new Vector3(0, 0, -1);
                break;
            case MapManager.DIRECTION.TOP:
                currentPlayer.transform.forward = new Vector3(0, 0, 1);
                break;
        }
    }

    virtual public void move(CharController currentPlayer)
    {
        currentPlayer.animator.SetTrigger("Forward");
        Vector2Int pos = currentPlayer.tablePosition;
        switch (currentPlayer.orientation)
        {
            case MapManager.DIRECTION.RIGHT:
                ++pos.x;
                break;
            case MapManager.DIRECTION.LEFT:
                --pos.x;
                break;
            case MapManager.DIRECTION.BOTTOM:
                ++pos.y;
                break;
            case MapManager.DIRECTION.TOP:
                --pos.y;
                break;
        }
        aux = pos;
    }

 }
