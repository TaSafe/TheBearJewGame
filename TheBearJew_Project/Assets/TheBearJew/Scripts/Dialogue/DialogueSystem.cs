using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }
    
    [SerializeField] private List<DialogueCharacterImage> _characterImages;

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

            UiHUD.instance.DialogueShow(true);

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

            //Atualiza a caixa de diálogo
            UiHUD.instance.DialogueChangeTexts(
                    sequence.Dialogues[_dialogueIndex].characterName.ToString(),
                    sequence.Dialogues[_dialogueIndex].dialogueText,
                    characterImg);

            _dialogueIndex++;
        }
        else
            HasEndedSequence = true;

        //Reseta diálogo após finalizar
        if (HasEndedSequence)
        {
            _dialogueIndex = 0;
            HasStartedDialogue = false;
            UiHUD.instance.DialogueChangeTexts(string.Empty, string.Empty, UiHUD.instance.HudWeaponImageDefault);
            UiHUD.instance.DialogueShow(false);
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
        return UiHUD.instance.HudWeaponImageDefault;
    }
}