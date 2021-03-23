using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    void Update() => Aiming();

    private void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _targetLayer))
            transform.LookAt(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
    }

}