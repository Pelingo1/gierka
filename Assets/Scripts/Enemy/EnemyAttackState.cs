using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        var Distance = Vector3.Distance(enemy.transform.position, enemy.playerRef.position);

        switch ((enemy.enemyFov.canSeePlayer, Distance < 3))
        {
            case (true, true):
                enemy.agent.SetDestination(enemy.transform.position);
                enemy.transform.LookAt(enemy.playerRef);
                //tutaj bedzie animacja ataku
                break;
            case (true, false):
                enemy.SwitchState(enemy.ChaseState);
                break;
            default:
                enemy.SwitchState(enemy.SuspictionState);
                break;
        }
    }
}
