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
        //Para parâmetros trigger o 'GetMouseButton' pode registrar que está precionado por mais de um frame, por isso o 'GetMouseButtonDown'
        if (_weaponHandler._weaponEquiped == PlayerWeaponHandler.WeaponEquiped.gun)
        {
            if (Input.GetMouseButton(0))
                _weaponHandler.Attack();
        }
        else if (_weaponHandler._weaponEquiped == PlayerWeaponHandler.WeaponEquiped.bat)
        {
            //Isso foi feito para que o ataque do bastão não seja disparado errado
            if (Input.GetMouseButtonDown(0))
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

        if (Input.GetKeyDown(KeyCode.E))
            _weaponHandler.SwitchWeapons();

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
