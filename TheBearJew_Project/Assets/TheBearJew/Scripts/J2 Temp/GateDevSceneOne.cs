using UnityEngine;

public class GateDevSceneOne : Gate, IInteraction
{
    [SerializeField] private DialogueSequence dialogueDontHaveKey;
    [SerializeField] private DialogueSequence dialogueDontHaveNecessaryKey;

    public void IdleInteraction() { }
    public void Interacting() { }
    public void Interaction() => GateActions();

    public override void GateActions()
    {
        if (CheckKey())
            return;
        else if (PlayerInput.instance.Inventory.HasItemOfType<Key>())
            DialogueSystem.instance.DialogueChanger(dialogueDontHaveNecessaryKey);
        else
            DialogueSystem.instance.DialogueChanger(dialogueDontHaveKey);
    }
}