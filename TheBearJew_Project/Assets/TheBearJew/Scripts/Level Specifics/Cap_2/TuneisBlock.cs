using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneisBlock : Gate, IInteraction
{
    [SerializeField] private GameObject _dynamiteModel;
    [SerializeField] private DialogueSequence _dialogueBeforeDynamite;
    [SerializeField] private DialogueSequence _dialogueAfterDynamite;
    public void Interaction() => GateActions();

    public override void GateActions()
    {
        if (ManagerTuneis.Instance.IsDynamiteInPlace)
        {
            DialogueSystem.Instance.DialogueChanger(_dialogueAfterDynamite);
            return;
        }

        if (CheckKeyInPlayerInventary())
        {
            RemoveKeyFromPlayerInventary();
            _dynamiteModel.SetActive(true);
            ManagerTuneis.Instance.IsDynamiteInPlace = true;
            DialogueSystem.Instance.DialogueChanger(_dialogueAfterDynamite);
        }
        else
        {
            DialogueSystem.Instance.DialogueChanger(_dialogueBeforeDynamite);
        }
    }

}