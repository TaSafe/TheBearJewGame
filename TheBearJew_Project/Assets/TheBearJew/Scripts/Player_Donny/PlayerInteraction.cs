using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //private Camera _mainCamera;
    private bool _ableToInteract;
    IInteraction _currentInteraction = null;

    private void Start()
    {
        //_mainCamera = Camera.main;
        Physics.IgnoreCollision(GetComponentInParent<CharacterController>(), gameObject.GetComponent<Collider>());
    }

    void Update()
    {
        //if (GetMouseHit(_mainCamera)?.GetComponent<IInteraction>() != null)
        //{
        //    if (Input.GetMouseButtonDown(1))
        //        GetMouseHit(_mainCamera).GetComponent<IInteraction>()?.Interaction();
        //}


        if (_ableToInteract && Input.GetKeyDown(KeyCode.E))
            _currentInteraction.Interaction();
    }

    #region Trriger enter/exit
    void OnTriggerEnter(Collider other)
    {
        _currentInteraction = other.GetComponent<IInteraction>();
        if (_currentInteraction != null)
        {
            _currentInteraction.Interacting();
            _ableToInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteraction = other.GetComponent<IInteraction>();
        if (_currentInteraction != null)
        {
            _currentInteraction.IdleInteraction();
            _ableToInteract = false;
        }
    }
    #endregion

    private Collider GetMouseHit(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
            return hitInfo.collider;
        else
            return null;
    }

}
