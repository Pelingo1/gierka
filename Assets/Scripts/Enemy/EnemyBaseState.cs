using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager enemy);

    public abstract void UpdateState(EnemyStateManager enemy);

}