using System.Collections;
using UnityEngine;

public class NodePlayerNear : BTNode
{
    private float _circleRadius;

    public NodePlayerNear(float circleRadius) => _circleRadius = circleRadius;

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (Physics.SphereCast(new Ray(root.transform.position, root.transform.forward), _circleRadius, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Player"))
                status = Status.SUCCESS;
        }
        //if (Vector3.Distance(root.transform.position, PlayerInput.Instance.transform.position) < _circleRadius)
        //    status = Status.SUCCESS;

        Print();
        yield break;
    }
}