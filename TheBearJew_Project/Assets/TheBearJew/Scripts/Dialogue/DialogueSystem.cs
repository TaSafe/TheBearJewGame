using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public bool HasEndedSequence { get; private set; }

    private bool _hasStartedDialogue;
    private int _dialogueIndex;

    public void DialogueChanger(DialogueSequence sequence)
    {
        if (!_hasStartedDialogue)
        {
            //Abrir a janela de dialogo
            UiHUD.instance.DialogueShow(true);

            //Habilitar trocar diálogos
            HasEndedSequence = false;
            _hasStartedDialogue = true;
        }

        if (_dialogueIndex < sequence.Dialogues.Count && !HasEndedSequence && _hasStartedDialogue)
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
            _hasStartedDialogue = false;
            UiHUD.instance.DialogueChangeTexts(string.Empty, string.Empty);
            UiHUD.instance.DialogueShow(false);
        }
    }
}