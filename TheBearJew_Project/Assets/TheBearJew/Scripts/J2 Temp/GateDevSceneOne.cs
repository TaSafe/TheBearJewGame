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
        {
            gameObject.SetActive(false);
            return;
        }
        else if (PlayerInput.Instance.Inventory.HasItemOfType<Key>())
            DialogueSystem.instance.DialogueChanger(dialogueDontHaveNecessaryKey);
        else
            DialogueSystem.instance.DialogueChanger(dialogueDontHaveKey);
    }
}