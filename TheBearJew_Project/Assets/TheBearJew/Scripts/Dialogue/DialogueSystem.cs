using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] DialogueSequence sequence;

    private void Start()
    {
        foreach (Dialogue dialogue in sequence.Dialogues)
        {
            Debug.Log($"{dialogue.characterName}: {dialogue.dialogueText}");
        }
        
    }

}