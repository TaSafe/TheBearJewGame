using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    private Camera _camera;

    private void Start() => _camera = Camera.main;

    public void Aiming()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _targetLayer))
            transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
    }
}