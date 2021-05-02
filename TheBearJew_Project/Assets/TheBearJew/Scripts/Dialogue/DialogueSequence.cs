using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Sequence", menuName = "Dialogue/Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    [SerializeField] private List<Dialogue> dialogues;
    public List<Dialogue> Dialogues { get { return dialogues; } }
}