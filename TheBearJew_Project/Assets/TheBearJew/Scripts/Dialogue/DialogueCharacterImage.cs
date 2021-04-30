using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Character Image", menuName = "Dialogue/Dialogue Character Image")]
public class DialogueCharacterImage : ScriptableObject
{
    [SerializeField] private DialogueCharacterName _characterName;
    [SerializeField] private Sprite _image;

    public DialogueCharacterName CharacterName { get { return _characterName; } }
    public Sprite CharacterImage { get { return _image; } }
}