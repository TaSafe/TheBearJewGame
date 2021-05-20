using UnityEngine;

public class MathUtilities : MonoBehaviour
{
    public static bool ConeChecker(GameObject mainGameObject, Vector3 objectToCompare, float coneAngle)
    {
        float cosAngle = Vector3.Dot(
            (objectToCompare - mainGameObject.transform.position).normalized,
            mainGameObject.transform.forward.normalized);

        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

        return angle < coneAngle;
    }
}
