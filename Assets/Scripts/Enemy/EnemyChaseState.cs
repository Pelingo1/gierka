using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private Vector3 lastSeen;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.agent.speed = 4.0f;
        enemy.agent.SetDestination(enemy.playerRef.position);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        var Distance = Vector3.Distance(enemy.transform.position,enemy.playerRef.position);

        switch((enemy.enemyFov.canSeePlayer, Distance<3))
        {
            case (true, true):
                enemy.SwitchState(enemy.AttackState);
                break;
            case (true, false):
                enemy.agent.SetDestination(enemy.playerRef.position);
                enemy.transform.LookAt(enemy.playerRef);
                lastSeen = enemy.PlayerPoint.position;
                break;
            default:
                enemy.agent.SetDestination(lastSeen);
                if(Vector3.Distance(lastSeen,enemy.EnemyPoint.position)<0.1f)
                    enemy.SwitchState(enemy.SuspictionState);
                break;
        }
    }
}
