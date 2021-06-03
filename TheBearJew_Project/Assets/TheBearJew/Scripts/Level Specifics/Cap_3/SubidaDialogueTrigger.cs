using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubidaDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSequence dialogueSequence;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogueSystem.Instance.DialogueChanger(dialogueSequence);
            GetComponent<Collider>().enabled = false;
        }
    }
}
