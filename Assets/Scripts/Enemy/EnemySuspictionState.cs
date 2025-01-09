using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySuspictionState : EnemyBaseState
{
    float awarness = 0f;

    public override void EnterState(EnemyStateManager enemy)
    {
        awarness = 0f;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.enemyFov.canSeePlayer)
        {
            awarness += 0.2f;
            enemy.transform.LookAt(enemy.playerRef);
        }
        else
            awarness -= 0.2f;

        Debug.Log(awarness);

        if (awarness >= 100)
            enemy.SwitchState(enemy.ChaseState);
        else if(awarness <= 0)
            enemy.SwitchState(enemy.StationaryState);
    }
}
