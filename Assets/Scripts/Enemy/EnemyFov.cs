using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFov : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;
    public Player playerComponent;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerComponent = playerRef.GetComponent<Player>();
        StartCoroutine(FovRoutine());
    }

    void Update()
    {
    }

    private IEnumerator FovRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        float radiusAdj = radius * playerComponent.visibility;

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusAdj, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
