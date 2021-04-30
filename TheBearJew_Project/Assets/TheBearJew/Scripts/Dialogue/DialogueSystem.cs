using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public bool HasEndedSequence { get; private set; }
    public bool HasStartedDialogue { get; private set; }

    private int _dialogueIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void DialogueChanger(DialogueSequence sequence)
    {
        if (!HasStartedDialogue)
        {
            //Abrir a janela de dialogo
            UiHUD.instance.DialogueShow(true);

            //Habilitar trocar diálogos
            HasEndedSequence = false;
            HasStartedDialogue = true;
        }

        if (_dialogueIndex < sequence.Dialogues.Count && !HasEndedSequence && HasStartedDialogue)
        {
            UiHUD.instance.DialogueChangeTexts(
                sequence.Dialogues[_dialogueIndex].characterName.ToString(), 
                sequence.Dialogues[_dialogueIndex].dialogueText);

            _dialogueIndex++;
        }
        else
            HasEndedSequence = true;

        if (HasEndedSequence)
        {
            _dialogueIndex = 0;
            HasStartedDialogue = false;
            UiHUD.instance.DialogueChangeTexts(string.Empty, string.Empty);
            UiHUD.instance.DialogueShow(false);
        }
    }

    public void ResetDialogue() => HasEndedSequence = false;

}