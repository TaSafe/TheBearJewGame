using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool _ableToInteract;
    IInteraction _currentInteraction = null;
    PlayerWeaponHandler _gunHandler;

    private void Start()
    {
        Physics.IgnoreCollision(GetComponentInParent<CharacterController>(), gameObject.GetComponent<Collider>());
        _gunHandler = GetComponentInParent<PlayerWeaponHandler>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_gunHandler.HasGun)
                _gunHandler.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_ableToInteract && !_gunHandler.HasGun)
            {
                _currentInteraction.Interaction();
                _ableToInteract = false;
                return;
            }
            else if(!_ableToInteract && _gunHandler.HasGun)
            {
                _gunHandler.DropGun();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        _currentInteraction = other.GetComponent<IInteraction>();
        if (_currentInteraction != null)
        {
            UiInteraction.instance.ShowUi(true);
            _currentInteraction.Interacting();
            _ableToInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteraction = other.GetComponent<IInteraction>();
        if (_currentInteraction != null)
        {
            UiInteraction.instance.ShowUi(false);
            _currentInteraction.IdleInteraction();
            _ableToInteract = false;
        }
    }

}
