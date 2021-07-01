using UnityEngine;

public abstract class Weapon : MonoBehaviour, IInteraction
{
    [SerializeField] private WeaponData _weaponData;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 _inHandPos;
    [SerializeField] private Vector3 _inHandRot;

    [Header("VFX")]
    [SerializeField] protected GameObject _vfxMuzzleFlash;
    [SerializeField] protected GameObject _vfxHit;
    [SerializeField] protected GameObject _vfxInteraction;

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GUN;

    public WeaponData WeaponData { get { return _weaponData; } }
    public Transform Muzzle { get; private set; }
    public int AmmoCurrent { get; private set; }
    public Vector3 ParentInHandPosition { get { return _inHandPos; } }
    public Vector3 ParentInHandRotation { get { return _inHandRot; } }

    protected void WeaponInit()
    {
        AmmoCurrent = WeaponData.MaxAmmo;
        Muzzle = Player.Instance.PlayerWeaponHandler.Muzzle.transform;
    }

    public abstract void Attack();

    //AmmoCurrent -= resultado do if.              AmmoCurrent -= if(ammoToReduce > 0) ammoToReduce else 0 
    protected void AmmoReduce(int ammoToReduce) => AmmoCurrent -= ammoToReduce > 0 ? ammoToReduce : 0;

    public void Interaction()
    {
        if (Player.Instance.PlayerWeaponHandler.HasGun)
        {
            if (Player.Instance.PlayerWeaponHandler._weaponEquiped == PlayerWeaponHandler.WeaponEquiped.BAT)
                Player.Instance.PlayerWeaponHandler.SwitchWeapons();

            Player.Instance.PlayerWeaponHandler.DropGun();
        }

        UiHUD.Instance.ShowIntereactionUI(false);
        GetComponent<Collider>().enabled = false;
        VfxInteractionSetActive(false);

        Player.Instance.PlayerWeaponHandler.EquipGun(gameObject, ParentInHandPosition, ParentInHandRotation);
    }

    public void VfxInteractionSetActive(bool state) => _vfxInteraction?.SetActive(state);
}