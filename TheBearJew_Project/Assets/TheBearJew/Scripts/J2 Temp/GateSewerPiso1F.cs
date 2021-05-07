using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateSewerPiso1F : Gate, IInteraction
{
    [SerializeField] private DialogueSequence dialogueDontHaveCrowbar;

    public void IdleInteraction() { }
    public void Interacting() { }
    public void Interaction() => GateActions();

    public override void GateActions()
    {
        if (GameManagerPJDois.instance.HasEnteredSewer)
        {
            SceneManager.LoadScene("Piso_0F_Esgoto");
            return;
        }

        if (CheckKey())
        {
            GameManagerPJDois.instance.HasEnteredSewer = true;
            SceneManager.LoadScene("Piso_0F_Esgoto");
            return;
        }
        else
            DialogueSystem.instance.DialogueChanger(dialogueDontHaveCrowbar);
    }
}
