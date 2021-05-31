using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private GameObject _batPrefab;
    [SerializeField] private GameObject _handBone;

    public GameObject Muzzle { get { return _muzzle; } }
    public bool HasGun { get; private set; }
    public GameObject BatClone { get; private set; }

    private GameObject _gunEquiped;
    private Animator _animator;
    private const string HIT_BAT_ANIMATION = "hitBatAnimation";

    public enum WeaponEquiped { bat, gun }
    [HideInInspector] public WeaponEquiped _weaponEquiped;

    private void Start()
    {
        BatClone = Instantiate(_batPrefab);

        _animator = GetComponentInChildren<Animator>();

        Bat batScript = BatClone.GetComponent<Bat>();
        EquipGun(BatClone, batScript.ParentInHandPosition , batScript.ParentInHandRotation);
        
        UiHUD.Instance.HudWeaponImage(batScript.WeaponData.HudImage);
        UiHUD.Instance.HudWeaponAmmo(batScript.WeaponData.MaxAmmo);
    }
    
    public void Attack(bool mouseButtonDown)
    {
        //Para parâmetros trigger o 'GetMouseButton' pode registrar que está precionado por mais de um frame, por isso usar o 'GetMouseButtonDown'

        if (_weaponEquiped == WeaponEquiped.gun && !mouseButtonDown)
            _gunEquiped?.GetComponent<GunShoot>().MakeShoot();

        //Isso foi feito para que o ataque do bastão não seja disparado errado
        if (_weaponEquiped == WeaponEquiped.bat && mouseButtonDown) 
            BatClone.GetComponent<Weapon>().Attack();
    }

    public void SwitchWeapons()
    {
        if (!HasGun || _animator.GetInteger(HIT_BAT_ANIMATION) != 0) return;
        //O que os cases executam pode ser substituído por um método
        switch (_weaponEquiped)
        {
            case WeaponEquiped.bat:
                BatClone.SetActive(false);
                _gunEquiped.SetActive(true);
                _animator.SetBool("IsWithBat", false);
                UiHUD.Instance.HudChangeWeapon(
                    _gunEquiped.GetComponent<GunShoot>().AmmoCurrent, 
                    _gunEquiped.GetComponent<GunBehaviour>().WeaponData.HudImage,
                    BatClone.GetComponent<Weapon>().WeaponData.HudImage
                    );
                
                _weaponEquiped = WeaponEquiped.gun; //status
                break;
            case WeaponEquiped.gun:
                _gunEquiped.SetActive(false);
                BatClone.SetActive(true);
                _animator.SetBool("IsWithBat", true);
                UiHUD.Instance.HudChangeWeapon(
                    BatClone.GetComponent<Weapon>().WeaponData.MaxAmmo, 
                    BatClone.GetComponent<Weapon>().WeaponData.HudImage, 
                    _gunEquiped.GetComponent<GunBehaviour>().WeaponData.HudImage
                    );
                
                _weaponEquiped = WeaponEquiped.bat; //status
                break;
        }
    }

    public void EquipGun(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        if (HasGun) return;

        if (gun == BatClone)
        {
            SetGunToHand(gun, inHandPosition, inHandRotation);
            _weaponEquiped = WeaponEquiped.bat;
            _animator.SetBool("IsWithBat", true);
        }
        else
        {
            _gunEquiped = gun;
            BatClone.SetActive(false);
            _animator.SetBool("IsWithBat", false);
            SetGunToHand(_gunEquiped, inHandPosition, inHandRotation);

            UiHUD.Instance.HudChangeWeapon(
                gun.GetComponent<GunShoot>().AmmoCurrent, 
                gun.GetComponent<GunBehaviour>().WeaponData.HudImage, 
                BatClone.GetComponent<Weapon>().WeaponData.HudImage
                );

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
        _gunEquiped.transform.position = new Vector3(
            transform.position.x, 1.5f, transform.position.z);
        _gunEquiped.transform.localRotation = Quaternion.Euler(
            0f, 90f, 0f);

        if (_gunEquiped.GetComponent<GunShoot>().AmmoCurrent <= 0)
            Destroy(_gunEquiped);

        _gunEquiped = null;

        Bat batScript = BatClone.GetComponent<Bat>();
        BatClone.SetActive(true);

        UiHUD.Instance.HudChangeWeapon(
            batScript.WeaponData.MaxAmmo, 
            batScript.WeaponData.HudImage, 
            UiHUD.Instance.HudWeaponImageDefault
            );

        FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/drop_weapon");  //Som dropar arma

        _weaponEquiped = WeaponEquiped.bat;
        HasGun = false;
    }

    private void SetGunToHand(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        gun.transform.SetParent(_handBone.transform);
        gun.transform.localPosition = inHandPosition;
        gun.transform.localEulerAngles = inHandRotation;

        //var child = GetComponentsInChildren<Transform>();
        //GameObject neededChild = null;

        //for (int i = 0; i < child.Length; i++)
        //{
        //    if (child[i].Find("swat:RightHand") != null)
        //    {
        //        neededChild = child[i].gameObject;
        //        break;
        //    }
        //}

        //if (neededChild != null)
        //{
        //    gun.transform.SetParent(neededChild.transform);
        //    gun.transform.localPosition = inHandPosition;
        //    gun.transform.localEulerAngles = inHandRotation;
        //}
    }

}