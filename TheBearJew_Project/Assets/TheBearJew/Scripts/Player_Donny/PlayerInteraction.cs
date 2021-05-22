using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteraction _currentInteraction = null;

    private void Start() => Physics.IgnoreCollision(GetComponentInParent<CharacterController>(), gameObject.GetComponent<Collider>());
    
    public void WeaponHandlerInteraction(PlayerWeaponHandler playerWeaponHandler)
    {
        if (_currentInteraction != null /*&& !playerWeaponHandler.HasGun*/)
        {
            _currentInteraction.Interaction();
            UiHUD.instance.ShowIntereactionUI(false);

            if (DialogueSystem.instance.HasStartedDialogue) return;

            _currentInteraction = null;
        }
        else if (_currentInteraction == null && playerWeaponHandler.HasGun)
        {
            playerWeaponHandler.DropGun();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _currentInteraction = other.GetComponent<IInteraction>();
        if (_currentInteraction != null)
        {
            UiHUD.instance.ShowIntereactionUI(true);
            _currentInteraction.Interacting();
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    _currentInteraction = other.GetComponent<IInteraction>();
    //    if (_currentInteraction == null)
    //        UiHUD.instance.ShowIntereactionUI(false);
    //}

    private void OnTriggerExit(Collider other)
    {
        if (_currentInteraction != null)
        {
            UiHUD.instance.ShowIntereactionUI(false);
            _currentInteraction.IdleInteraction();
            _currentInteraction = null;
        }
    }
}
