using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] private List<DialogueCharacterImage> _characterImages;
    [FMODUnity.EventRef] [SerializeField] string _soundChangeDialogue;

    [HideInInspector] public UnityEvent OnDialogueEnd;

    public bool HasEndedSequence { get; private set; }
    public bool HasStartedDialogue { get; private set; }
    
    private int _dialogueIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DialogueChanger(DialogueSequence sequence)
    {
        //Inicia diálogo
        if (!HasStartedDialogue)
        {
            PlayerInput.Instance.DisableInput();

            UiHUD.Instance.DialogueShow(true);

            HasEndedSequence = false;
            HasStartedDialogue = true;
        }

        //Troca diálogo
        if (_dialogueIndex < sequence.Dialogues.Count && !HasEndedSequence && HasStartedDialogue)
        {
            //Imagem do personagem
            Sprite characterImg = null;
            if (_dialogueIndex == 0)    //SEI LÁ..... FUNCIONA.
                characterImg = CharacterImageFinder(sequence);
            else if (sequence.Dialogues[_dialogueIndex - 1].characterName != sequence.Dialogues[_dialogueIndex].characterName)
                characterImg = CharacterImageFinder(sequence);

            //Som de troca de diálogo
            FMODUnity.RuntimeManager.PlayOneShot(_soundChangeDialogue);

            //Atualiza a caixa de diálogo
            string characterName = sequence.Dialogues[_dialogueIndex].characterName.ToString().Replace("_", " ");

            UiHUD.Instance.DialogueChangeTexts(
                    characterName,
                    sequence.Dialogues[_dialogueIndex].dialogueText,
                    characterImg);

            _dialogueIndex++;
        }
        else
            HasEndedSequence = true;

        //Reseta diálogo após finalizar
        if (HasEndedSequence)
        {
            OnDialogueEnd?.Invoke();
            _dialogueIndex = 0;
            HasStartedDialogue = false;
            UiHUD.Instance.DialogueChangeTexts(string.Empty, string.Empty, UiHUD.Instance.HudWeaponImageDefault);
            UiHUD.Instance.DialogueShow(false);
            PlayerInput.Instance.EnableInput();

            HasEndedSequence = false;   //Reseta a sequência
        }
    }

    private Sprite CharacterImageFinder(DialogueSequence sequence)
    {
        foreach (DialogueCharacterImage character in _characterImages)
        {
            if (character.CharacterName == sequence.Dialogues[_dialogueIndex].characterName)
                return character.CharacterImage;
        }
        return UiHUD.Instance.HudWeaponImageDefault;
    }
}