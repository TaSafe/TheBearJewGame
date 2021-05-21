using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NodeApproximationToPlayer : BTNode
{
    private float _attackRange;
    private NavMeshAgent _navMeshAgent;

    public NodeApproximationToPlayer(float attackRange, NavMeshAgent navMeshAgent)
    {
        _attackRange = attackRange;
        _navMeshAgent = navMeshAgent;
    }

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.RUNNING;

        if (Vector3.Distance(root.transform.position, PlayerInput.Instance.transform.position) > _attackRange)
            _navMeshAgent.SetDestination(PlayerInput.Instance.transform.position);
        else
        {
            _navMeshAgent.ResetPath();
            status = Status.SUCCESS;
        }

        //Print();

        yield return new WaitForSeconds(.2f);
    }
}