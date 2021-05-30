using UnityEngine;

public class DialogueTest : MonoBehaviour, IInteraction
{
    [SerializeField] private DialogueSequence _sequence;

    public void Interaction() => DialogueSystem.Instance.DialogueChanger(_sequence);
}
