using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    IInteraction _currentInteraction = null;
    PlayerWeaponHandler _weaponHandler;

    private void Start()
    {
        Physics.IgnoreCollision(GetComponentInParent<CharacterController>(), gameObject.GetComponent<Collider>());
        _weaponHandler = GetComponentInParent<PlayerWeaponHandler>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_weaponHandler.HasGun)
                _weaponHandler.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_currentInteraction != null && !_weaponHandler.HasGun)
            {
                _currentInteraction.Interaction();
                _currentInteraction = null;
            }
            else if(_currentInteraction == null && _weaponHandler.HasGun)
            {
                _weaponHandler.DropGun();
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
