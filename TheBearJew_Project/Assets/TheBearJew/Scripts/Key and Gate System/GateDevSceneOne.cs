using UnityEngine;

public class GateDevSceneOne : Gate
{
    [SerializeField] private DialogueSequence dialogueDontHaveKey;
    [SerializeField] private DialogueSequence dialogueDontHaveNecessaryKey;

    public override void Interaction() => GateActions();

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