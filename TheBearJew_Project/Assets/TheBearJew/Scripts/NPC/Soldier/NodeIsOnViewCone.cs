using System.Collections;
using UnityEngine;

public class NodeIsOnViewCone : BTNode
{
    private float _coneAngle;
    private float _coneDistance;

    public NodeIsOnViewCone(float coneAngle, float coneDistance)
    {
        _coneAngle = coneAngle;
        _coneDistance = coneDistance;
    }

    public override IEnumerator Run(BTRoot root)
    {
        status = Status.FAILURE;

        if (MathUtilities.ConeChecker(root.gameObject, PlayerInput.Instance.gameObject.transform.position, _coneAngle))
        {
            Vector3 rayOrigin = new Vector3(
                root.gameObject.transform.position.x, 
                root.gameObject.transform.position.y + 1.5f, 
                root.gameObject.transform.position.z);

            Ray ray = new Ray(rayOrigin, PlayerInput.Instance.gameObject.transform.position - rayOrigin);
            
            Debug.DrawRay(ray.origin, ray.direction * _coneDistance, Color.blue);
            
            if (Physics.Raycast(ray, out RaycastHit hit, _coneDistance))
            {
                if (hit.collider.CompareTag("Player"))
                    status = Status.SUCCESS;
            }
        }

        Print();

        yield break;
    }
}