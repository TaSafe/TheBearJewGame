using UnityEngine;

public class GunBehaviour : MonoBehaviour, IInteraction
{

    [SerializeField] private GunData _gunData;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public void IdleInteraction() { }

    public void Interacting() { }

    public void Interaction()
    {
        UiInteraction.instance.ShowUi(false);
        UiInteraction.instance.GunHudImage(_gunData.HudImage);
        GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGunHandler>().EquipGun(gameObject, inHandPos, inHandRot);
    }
}
