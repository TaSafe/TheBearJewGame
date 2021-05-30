using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneisEndGate : Gate
{


    public override void Interaction() => GateActions();
    
    public override void GateActions()
    {
        if(CheckKeyInPlayerInventary())
        {
            RemoveKeyFromPlayerInventary();
            gameObject.SetActive(false);
        }
    }

}
