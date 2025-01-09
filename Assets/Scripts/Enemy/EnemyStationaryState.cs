using System.Collections;
using UnityEngine;

public class EnemyStationaryState : EnemyBaseState
{
    private Quaternion WpRotation;
    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.agent.speed = 2.0f;
        enemy.agent.SetDestination(enemy.patrolPoints[enemy.targetPoint].position);
        WpRotation = enemy.patrolPoints[0].rotation;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        switch (enemy.enemyFov.canSeePlayer)
        {
            case (true):
                enemy.agent.SetDestination(enemy.transform.position);
                enemy.transform.LookAt(enemy.playerRef);
                enemy.SwitchState(enemy.SuspictionState);
                break;

            default:
                if (Vector3.Distance(enemy.EnemyPoint.position, enemy.patrolPoints[enemy.targetPoint].position) < 0.1f)
                {
                    if (enemy.patrolPoints.Length > 1)
                    {
                        enemy.targetPoint++;
                        if (enemy.targetPoint >= enemy.patrolPoints.Length)
                        {
                            enemy.targetPoint = 0;
                        }
                        enemy.agent.SetDestination(enemy.patrolPoints[enemy.targetPoint].position);
                    }
                    else
                    {
                        if (enemy.transform.rotation != WpRotation)
                            enemy.transform.rotation = Quaternion.RotateTowards(enemy.transform.rotation, WpRotation, 60 * Time.deltaTime);
                    }

                }  
                break;
        }
    }
}
