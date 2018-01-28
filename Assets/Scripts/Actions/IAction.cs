using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction
{
    public Sprite spriteUI;
    private float m_acumTime;
    public float actionDuration = 1.0f;
    public abstract void preAction(CharController currentPlayer);
    public virtual void startAction(CharController currentPlayer)
    {
        running = true;
    }
    public void updateAction()
    {
        // Espera hasta que termina la jodida acción
        m_acumTime += Time.deltaTime;
        if (m_acumTime > actionDuration)
        {
            m_acumTime = 0;
            running = false;
        }
    }
    public abstract void postAction(CharController currentPlayer);

    public bool running { get; private set; }
}
