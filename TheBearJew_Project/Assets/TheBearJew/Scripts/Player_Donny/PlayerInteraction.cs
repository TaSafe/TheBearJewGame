using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (GetMouseHit(_mainCamera)?.GetComponent<IInteraction>() != null)
        {
            if (Input.GetMouseButtonDown(1))
                GetMouseHit(_mainCamera).GetComponent<IInteraction>()?.Interaction();
        }
    }

    private Collider GetMouseHit(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
            return hitInfo.collider;
        else
            return null;
    }

}
