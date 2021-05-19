using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NodeMoveToTarget : BTNode
{

    private NavMeshAgent _navMeshAgent;
    private List<GameObject> _targets = new List<GameObject>();
    private GameObject nullTarget;

    public NodeMoveToTarget(Transform defaultPosition, NavMeshAgent navMeshAgent, List<GameObject> targets)
    {
        _navMeshAgent = navMeshAgent;

        //if (targets == null)
        //{
        //    Vector3 newPos = new Vector3(defaultPosition.position.x, defaultPosition.position.y, defaultPosition.position.z);
        //    nullTarget = GameObject.Instantiate(new GameObject(), newPos, Quaternion.identity);
        //    _targets.Add(nullTarget);
        //}
        //else
            _targets = targets;
    }

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.RUNNING;

        _navMeshAgent.SetDestination(_targets[0].transform.position);

        while (_navMeshAgent.remainingDistance > .5f)
        {
            yield return null;

            status = Status.SUCCESS;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;

        Print();
    }
}