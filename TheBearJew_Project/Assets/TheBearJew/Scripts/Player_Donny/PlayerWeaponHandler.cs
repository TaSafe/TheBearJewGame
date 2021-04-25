using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private GameObject _batPrefab;

    public GameObject Muzzle { get { return _muzzle; } }
    public bool HasGun { get; private set; }

    private GameObject _gunEquiped;
    private GameObject _batClone;

    public enum WeaponEquiped { bat, gun }
    [HideInInspector] public WeaponEquiped _weaponEquiped;

    private void Start()
    {
        _batClone = Instantiate(_batPrefab);
        
        Bat batScript = _batClone.GetComponent<Bat>();
        EquipGun(_batClone, batScript.ParentInHandPosition , batScript.ParentInHandRotation);
        
        UiInteraction.instance.GunHudImage(batScript.WeaponData.HudImage);
        UiInteraction.instance.HudGunAmmo(batScript.WeaponData.MaxAmmo);
    }
    
    public void Attack()
    {
        if (_weaponEquiped == WeaponEquiped.gun)
            _gunEquiped?.GetComponent<GunShoot>().MakeShoot();

        if (_weaponEquiped == WeaponEquiped.bat) 
            _batClone.GetComponent<Weapon>().Attack();
    }

    public void SwitchWeapons()
    {
        if (!HasGun) return;

        switch (_weaponEquiped)
        {
            case WeaponEquiped.bat:
                _batClone.SetActive(false);
                _gunEquiped.SetActive(true);
                _weaponEquiped = WeaponEquiped.gun;
                break;
            case WeaponEquiped.gun:
                _gunEquiped.SetActive(false);
                _batClone.SetActive(true);
                _weaponEquiped = WeaponEquiped.bat;
                break;
        }
    }

    public void EquipGun(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        if (HasGun) return;

        if (gun == _batClone)
        {
            SetGunToHand(gun, inHandPosition, inHandRotation);
            _weaponEquiped = WeaponEquiped.bat;
        }
        else
        {
            _gunEquiped = gun;
            _batClone.SetActive(false);
            SetGunToHand(_gunEquiped, inHandPosition, inHandRotation);

            //Som pegar arma
            FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/pick_weapon");

            _weaponEquiped = WeaponEquiped.gun;
            HasGun = true;
        }
    }

    public void DropGun()
    {
        if (_weaponEquiped != WeaponEquiped.gun) return;

        _gunEquiped.GetComponent<Collider>().enabled = true;
        _gunEquiped.transform.parent = null;
        _gunEquiped = null;

        Bat batScript = _batClone.GetComponent<Bat>();
        _batClone.SetActive(true);

        _weaponEquiped = WeaponEquiped.bat;

        UiInteraction.instance.GunHudImage(batScript.WeaponData.HudImage);   //MUDA A ARMA EXIBIDA NO HUD
        UiInteraction.instance.HudGunAmmo(batScript.WeaponData.MaxAmmo);   //MUDA A ARMA EXIBIDA NO HUD

        //Som dropar arma
        FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/drop_weapon");

        HasGun = false;
    }

    private void SetGunToHand(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        var child = GetComponentsInChildren<Transform>();
        GameObject neededChild = null;

        for (int i = 0; i < child.Length; i++)
        {
            if (child[i].Find("swat:RightHand") != null)
            {
                neededChild = child[i].gameObject;
                break;
            }
        }

        if (neededChild != null)
        {
            gun.transform.SetParent(neededChild.transform);
            gun.transform.localPosition = inHandPosition;
            gun.transform.localEulerAngles = inHandRotation;
        }
    }
}
