using System.Collections;
using UnityEngine;

public class NodeIsOnViewCone : BTNode
{
    private float _coneAngle;

    public NodeIsOnViewCone(float coneAngle) => _coneAngle = coneAngle;

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (MathUtilities.ConeChecker(root.gameObject, PlayerInput.Instance.gameObject.transform.position, _coneAngle))
            status = Status.SUCCESS;

        yield break;
    }
}