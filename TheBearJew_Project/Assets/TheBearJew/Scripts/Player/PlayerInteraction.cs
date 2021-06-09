using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public IInteraction CurrentInteraction { get; private set; } = null;

    private void Start() => Physics.IgnoreCollision(GetComponentInParent<CharacterController>(), gameObject.GetComponent<Collider>());
    
    public void WeaponHandlerInteraction(PlayerWeaponHandler playerWeaponHandler)
    {
        if (CurrentInteraction != null)
        {
            CurrentInteraction.Interaction();
            UiHUD.Instance.ShowIntereactionUI(false);

            if (DialogueSystem.Instance != null)
            {
                if (DialogueSystem.Instance.HasStartedDialogue) return;
            }

            CurrentInteraction = null;
        }
        else if (CurrentInteraction == null && playerWeaponHandler.HasGun)
            playerWeaponHandler.DropGun();
    }

    private void OnTriggerStay(Collider other)
    {
        if (CurrentInteraction == null)
            CurrentInteraction = other.GetComponent<IInteraction>();

        if (CurrentInteraction != null)
        {
            bool isMouse = false;

            if (CurrentInteraction.MyType == IInteraction.InteractionType.GUN)
                isMouse = true;

            UiHUD.Instance.ShowIntereactionUI(true, isMouse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CurrentInteraction = null;
        UiHUD.Instance.ShowIntereactionUI(false);
    }
}
