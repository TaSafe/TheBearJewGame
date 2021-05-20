using System.Collections;
using UnityEngine;

public class NodeGameObjectNear : BTNode
{
    private float circleRadius;

    public NodeGameObjectNear(float circleRadius) => this.circleRadius = circleRadius;

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (Physics.SphereCast(new Ray(root.transform.position, root.transform.forward), circleRadius, out RaycastHit hit))
        {
            if (hit.collider.gameObject == PlayerInput.Instance.gameObject)
                status = Status.SUCCESS;
        }
        yield break;
    }
}