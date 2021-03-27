using System;
using UnityEngine;

public class GunPick : MonoBehaviour, IInteraction
{

    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public void IdleInteraction() { }

    public void Interacting() { }

    public void Interaction()
    {
        UiInteraction.instance.ShowUi(false);
        UiInteraction.instance.GunHudImage(GetComponent<GunBehaviours>().GetGunHudImage());
        GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGunHandler>().EquipGun(gameObject, inHandPos, inHandRot);
    }
}
