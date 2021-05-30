using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteTrigger : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _block;
    [SerializeField] private DialogueSequence _dialogueWithoutDynamite;
    
    [Header("SFX")]
    [FMODUnity.EventRef] [SerializeField] private string _explosionSound;
    [FMODUnity.EventRef] [SerializeField] private string _triggerNotWorkingSound;

    public void Interaction()
    {
        if (ManagerTuneis.Instance.IsDynamiteInPlace && !ManagerTuneis.Instance.HasBlockExploded)
        {
            FMODUnity.RuntimeManager.PlayOneShot(_explosionSound, _block.transform.position);
            ManagerTuneis.Instance.HasBlockExploded = true;
            _block.SetActive(false);

            return;
        }

        FMODUnity.RuntimeManager.PlayOneShot(_triggerNotWorkingSound);
        DialogueSystem.Instance.DialogueChanger(_dialogueWithoutDynamite);
    }
}
