using System.Collections;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _ableToInteract;
    private bool _hasGun;
    IInteraction _currentInteraction = null;

    private void Start() => Physics.IgnoreCollision(GetComponentInParent<CharacterController>(), gameObject.GetComponent<Collider>());

    //Tem que mudar como verifica se possui arma na mão

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && transform.parent.GetComponentInChildren<GunPick>() != null && _hasGun)
        {
            transform.parent.GetComponentInChildren<GunPick>().DropGun();
            _hasGun = false;
        }

        if (Input.GetMouseButtonDown(1) && _ableToInteract)
        {
            _currentInteraction.Interaction();
            _ableToInteract = false;
            _hasGun = true;
        }
    }

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

    private Collider GetMouseHitCollider(Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
            return hitInfo.collider;
        else
            return null;
    }

}
