using UnityEngine;

public class DialogueTest : MonoBehaviour, IInteraction
{
    [SerializeField] private DialogueSequence _sequence;

    public void IdleInteraction() { }

    public void Interacting() { }

    public void Interaction() => DialogueSystem.instance.DialogueChanger(_sequence);
}
