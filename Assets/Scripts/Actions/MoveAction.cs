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

    protected void usePizarrita(Vector2Int target)
    {
        Debug.Log("pizarrita con: " + target);
        if (pizarrita.ContainsKey(target))
        {
            pizarrita[target] = pizarrita[target] + 1;
        }
        else
        {
            pizarrita[target] = 1;
        }
    }

    public override void preAction(CharController currentPlayer)
    {
        //if (!currentPlayer.m_mapManager.canMove(currentPlayer.tablePosition, dir, out m_targetPosition))
        //{
        //    usePizarrita(m_target);// Se hace doble pizarrita para que parezca como que hay dos personas intentando
        //    // ir al mismo sitio y no se pueda.
        //}
    }

    protected bool canMove(CharController currentPlayer)
    {
        return currentPlayer.m_mapManager.canMove(currentPlayer.tablePosition, dir, out m_targetPosition);
       
    }

    public override void startAction(CharController currentPlayer)
    {
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
        pizarrita.Clear();
    }

    virtual public void move(CharController currentPlayer)
    {
        currentPlayer.animator.SetTrigger("Forward");
        currentPlayer.tablePosition = m_target;
    }

 }
