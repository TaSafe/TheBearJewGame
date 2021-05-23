using UnityEngine;

public class GateMainPiso1F : Gate, IInteraction
{
    [SerializeField] private DialogueSequence dialogueDontHaveKey;

    public void IdleInteraction() { }
    public void Interacting() { }
    public void Interaction() => GateActions();

    public override void GateActions()
    {
        if (CheckKeyInPlayerInventary())
        {
            GameStatus.Instance.HasOpenedEndGate = true;
            RemoveKeyFromPlayerInventary();
            gameObject.SetActive(false);
            return;
        }
        else
            DialogueSystem.instance.DialogueChanger(dialogueDontHaveKey);
    }
}
