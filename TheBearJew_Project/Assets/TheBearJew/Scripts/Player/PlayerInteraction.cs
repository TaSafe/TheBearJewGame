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
            UiHUD.Instance.ShowIntereactionUI(false);

            if (DialogueSystem.Instance != null)
            {
                if (DialogueSystem.Instance.HasStartedDialogue) return;
            }

            _currentInteraction = null;
        }
        else if (_currentInteraction == null && playerWeaponHandler.HasGun)
            playerWeaponHandler.DropGun();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_currentInteraction == null)
            _currentInteraction = other.GetComponent<IInteraction>();

        if (_currentInteraction != null)
            UiHUD.Instance.ShowIntereactionUI(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _currentInteraction = null;
        UiHUD.Instance.ShowIntereactionUI(false);
    }
}
