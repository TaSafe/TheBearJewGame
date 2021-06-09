using UnityEngine;

public class DialogueTest : MonoBehaviour, IInteraction
{
    [SerializeField] private DialogueSequence _sequence;

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;

    public void Interaction() => DialogueSystem.Instance.DialogueChanger(_sequence);
}