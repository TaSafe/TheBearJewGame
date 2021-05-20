using System.Collections;
using UnityEngine;

public class NodeGameObjectNear : BTNode
{
    private float circleRadius;
    private GameObject gameObjectToCheck;

    public NodeGameObjectNear(float parameters, GameObject go)
    {
        circleRadius = parameters;
        gameObjectToCheck = go;
    }

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (Physics.SphereCast(new Ray(root.transform.position, root.transform.forward), circleRadius, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObjectToCheck)
                status = Status.SUCCESS;
        }

        Print();

        yield break;
    }
}