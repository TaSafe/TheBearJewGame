using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
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
            if (_currentInteraction != null && !_gunHandler.HasGun)
            {
                _currentInteraction.Interaction();
                _currentInteraction = null;
            }
            else if(_currentInteraction == null && _gunHandler.HasGun)
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_currentInteraction != null)
        {
            UiInteraction.instance.ShowUi(false);
            _currentInteraction.IdleInteraction();
            _currentInteraction = null;
        }
    }

}
