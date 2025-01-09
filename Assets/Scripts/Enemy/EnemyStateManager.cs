using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    public EnemyStationaryState StationaryState = new EnemyStationaryState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemySuspictionState SuspictionState = new EnemySuspictionState();
    public EnemyAttackState AttackState = new EnemyAttackState();

    public Transform[] patrolPoints;
    public int targetPoint;

    public EnemyFov enemyFov;
    public NavMeshAgent agent;
    public Transform playerRef;
    public Transform EnemyPoint;
    public Transform PlayerPoint;

    void Start()
    {
        enemyFov = GetComponent<EnemyFov>();
        playerRef = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        targetPoint = 0;

        currentState = StationaryState;
        currentState.EnterState(this);
    }

    public void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}