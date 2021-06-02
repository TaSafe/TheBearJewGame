using UnityEngine;

public class MathUtilities : MonoBehaviour
{
    public static bool ConeChecker(GameObject mainGameObject, Vector3 objectToCompare, float coneAngle)
    {
        float cosAngle = Vector3.Dot(
            (objectToCompare - mainGameObject.transform.position).normalized,
            mainGameObject.transform.forward.normalized);

        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

        Debug.DrawRay(mainGameObject.transform.position, objectToCompare * 50f, Color.green);

        return angle < coneAngle;
    }

    public static float ConeAngle(GameObject mainGameObject, Vector3 objectToCompare)
    {
        float cosAngle = Vector3.Dot(
            (objectToCompare - mainGameObject.transform.position).normalized,
            mainGameObject.transform.forward.normalized);

        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        Debug.Log(cosAngle);
        return cosAngle;
    }
}
