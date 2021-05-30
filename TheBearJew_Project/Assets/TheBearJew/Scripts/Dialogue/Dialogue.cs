using UnityEngine;
[System.Serializable]
public struct Dialogue
{
    public DialogueCharacterName characterName;
    [TextArea(2, 6)] public string dialogueText;
}