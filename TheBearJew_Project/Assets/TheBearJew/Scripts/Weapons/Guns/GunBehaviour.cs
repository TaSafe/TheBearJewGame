using UnityEngine;

public class GunBehaviour : MonoBehaviour, IInteraction
{

    [SerializeField] private WeaponData _gunData;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public WeaponData GunData { get { return _gunData; } }
    public Transform Muzzle { get; private set; }

    private void Start()
    {
        Muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform;
    }

    public void IdleInteraction() { }

    public void Interacting() { }

    public void Interaction()
    {
        UiInteraction.instance.ShowUi(false);
        UiInteraction.instance.GunHudImage(_gunData.HudImage);
        UiInteraction.instance.HudGunAmmo(GetComponent<GunShoot>().AmmoCurrent);  //Maneira de adquirir valor temporária
        GetComponent<Collider>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerWeaponHandler>().EquipGun(gameObject, inHandPos, inHandRot);
    }
}
