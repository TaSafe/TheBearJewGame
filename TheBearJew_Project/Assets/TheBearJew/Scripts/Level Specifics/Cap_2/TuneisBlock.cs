using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneisBlock : Gate, IInteraction
{
    [SerializeField] private GameObject _dynamiteModel;

    public override void GateActions()
    {
        if (ManagerTuneis.Instance.IsDynamiteInPlace) return;

        if (CheckKeyInPlayerInventary())
        {
            RemoveKeyFromPlayerInventary();
            _dynamiteModel.SetActive(true);
            ManagerTuneis.Instance.IsDynamiteInPlace = true;
        }
    }

    public void Interaction() => GateActions();
    public void IdleInteraction() { }
    public void Interacting() { }
}