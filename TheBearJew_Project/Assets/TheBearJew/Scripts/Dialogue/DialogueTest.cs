using UnityEngine;

public class DialogueTest : MonoBehaviour, IInteraction
{
    [SerializeField] private DialogueSequence _sequence;

    public void IdleInteraction() { }

    public void Interacting() { }

    public void Interaction()
    {
        if (DialogueSystem.instance.HasEndedSequence)
            DialogueSystem.instance.DialogueReset();

        if (!DialogueSystem.instance.HasEndedSequence)
            DialogueSystem.instance.DialogueChanger(_sequence);
    }
}
