using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    [SerializeField] private DialogueSequence _sequence;

    private DialogueSystem _dialogueSystem;

    private void Start()
    {
        _dialogueSystem = GetComponent<DialogueSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_dialogueSystem.HasEndedSequence)
        {
            _dialogueSystem.DialogueChanger(_sequence);
        }
    }
}
