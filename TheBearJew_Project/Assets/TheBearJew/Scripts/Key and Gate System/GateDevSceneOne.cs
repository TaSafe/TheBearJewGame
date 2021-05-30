using UnityEngine;

public class GateDevSceneOne : Gate, IInteraction
{
    [SerializeField] private DialogueSequence dialogueDontHaveKey;
    [SerializeField] private DialogueSequence dialogueDontHaveNecessaryKey;

    public void Interaction() => GateActions();

    public override void GateActions()
    {
        if (CheckKeyInPlayerInventary())
        {
            gameObject.SetActive(false);
            return;
        }
        else if (PlayerInput.Instance.Inventory.HasItemOfType<Key>())
            DialogueSystem.Instance.DialogueChanger(dialogueDontHaveNecessaryKey);
        else
            DialogueSystem.Instance.DialogueChanger(dialogueDontHaveKey);
    }
}