using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    private Camera _camera;

    private void Start() => _camera = Camera.main;

    void Update() => Aiming();

    private void Aiming()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _targetLayer))
        {
            transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));    // Old code to make character aim
            
            // The new code don't seen to be most effective then the old one
            //var _aimDirection = hitInfo.point - transform.position;
            //_aimDirection.y = 0f;
            //_aimDirection.Normalize();
            //transform.forward = _aimDirection;
        }
    }

}