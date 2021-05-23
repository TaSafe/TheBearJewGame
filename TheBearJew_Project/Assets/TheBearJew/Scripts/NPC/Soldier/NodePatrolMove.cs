using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NodePatrolMove : BTNode
{
    private NavMeshAgent _navMeshAgent;
    private List<Transform> _targets = new List<Transform>();
    private int _currentTarget;

    public NodePatrolMove(Transform defaultPosition, NavMeshAgent navMeshAgent, List<Transform> targets)
    {
        _navMeshAgent = navMeshAgent;

        if (targets == null || targets.Count == 0)
        {
            //_targets.Add(GameObject.Instantiate(game, defaultPosition.position, Quaternion.identity).transform);
            GameObject newTargetPoint = new GameObject($"{defaultPosition.gameObject.name} Target Point");
            newTargetPoint.transform.position = defaultPosition.position;
            _targets.Add(newTargetPoint.transform);
        }
        else
            _targets = targets;

        _currentTarget = 0;
    }

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (_targets[_currentTarget] != null)
        {
            _navMeshAgent.SetDestination(_targets[_currentTarget].position);

            while (Vector3.Distance(root.transform.position, _targets[_currentTarget].position) > .5f)
            {
                yield return new WaitForSeconds(.2f);   
            }
            _currentTarget++;
            if (_currentTarget > _targets.Count - 1) _currentTarget = 0;
        }

        Print();
    }
}