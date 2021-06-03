using UnityEngine;

public class SubidaDialogueTrigger : MonoBehaviour, IInteraction
{
    [SerializeField] private DialogueSequence dialogueSequence;
    private bool startDialogue;

    public void Interaction()
    {
        if (startDialogue)
            DialogueSystem.Instance.DialogueChanger(dialogueSequence);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !startDialogue)
        {
            startDialogue = true;
            DialogueSystem.Instance.DialogueChanger(dialogueSequence);
            DialogueSystem.Instance.OnDialogueEnd.AddListener(End);
        }
    }

    private void End()
    {
        GetComponent<Collider>().enabled = false;

        DialogueSystem.Instance.OnDialogueEnd.RemoveListener(End);
    }

}
