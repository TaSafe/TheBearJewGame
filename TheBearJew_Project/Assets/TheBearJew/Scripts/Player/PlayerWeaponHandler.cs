using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private GameObject _batPrefab;
    [SerializeField] private GameObject _handBone;

    public GameObject Muzzle { get { return _muzzle; } }
    public bool HasGun { get; private set; }
    public GameObject BatClone { get; private set; }
    public GunShoot CurrentGunShoot { get; private set; }

    public enum WeaponEquiped { BAT, GUN }
    [HideInInspector] public WeaponEquiped _weaponEquiped;

    private GameObject _gunEquiped;
    private Animator _animator;
    private const string IS_WITH_BAT = "IsWithBat";

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

        if (_weaponEquiped == WeaponEquiped.GUN && !mouseButtonDown && HasGun)
            _gunEquiped?.GetComponent<GunShoot>().MakeShoot(); //TODO: modificar para a referencia que o script faz ao equipar

        //Isso foi feito para que o ataque do bastão não seja disparado errado
        if (_weaponEquiped == WeaponEquiped.BAT && mouseButtonDown) 
            BatClone.GetComponent<Weapon>().Attack();
    }

    public void SwitchWeapons()
    {
        if (!HasGun) return;

        //O que os cases executam pode ser substituído por um método
        switch (_weaponEquiped)
        {
            case WeaponEquiped.BAT:

                BatClone.SetActive(false);
                _gunEquiped.SetActive(true);
                _animator.SetBool(IS_WITH_BAT, false);
                UiHUD.Instance.HudChangeWeapon(
                    _gunEquiped.GetComponent<GunShoot>().AmmoCurrent, 
                    _gunEquiped.GetComponent<GunBehaviour>().WeaponData.HudImage,
                    BatClone.GetComponent<Weapon>().WeaponData.HudImage );
                _weaponEquiped = WeaponEquiped.GUN; //status

                break;

            case WeaponEquiped.GUN:

                _gunEquiped.SetActive(false);
                BatClone.SetActive(true);
                _animator.SetBool(IS_WITH_BAT, true);
                UiHUD.Instance.HudChangeWeapon(
                    BatClone.GetComponent<Weapon>().WeaponData.MaxAmmo, 
                    BatClone.GetComponent<Weapon>().WeaponData.HudImage, 
                    _gunEquiped.GetComponent<GunBehaviour>().WeaponData.HudImage);
                _weaponEquiped = WeaponEquiped.BAT; //status

                break;
        }
    }

    public void EquipGun(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        if (HasGun) return;

        if (gun == BatClone)
        {
            SetGunToHand(gun, inHandPosition, inHandRotation);
            _weaponEquiped = WeaponEquiped.BAT;
            _animator.SetBool("IsWithBat", true);
        }
        else
        {
            _gunEquiped = gun;
            BatClone.SetActive(false);
            _animator.SetBool("IsWithBat", false);
            SetGunToHand(_gunEquiped, inHandPosition, inHandRotation);

            CurrentGunShoot = _gunEquiped.GetComponent<GunShoot>();

            UiHUD.Instance.HudChangeWeapon(
                gun.GetComponent<GunShoot>().AmmoCurrent, 
                gun.GetComponent<GunBehaviour>().WeaponData.HudImage, 
                BatClone.GetComponent<Weapon>().WeaponData.HudImage
                );

            //Som pegar arma
            FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/pick_weapon");

            _weaponEquiped = WeaponEquiped.GUN;
            HasGun = true;
        }
    }

    public void DropGun()
    {
        if (_weaponEquiped != WeaponEquiped.GUN) return;

        _gunEquiped.GetComponent<Collider>().enabled = true;
        _gunEquiped.GetComponent<GunBehaviour>().interactableVFX.SetActive(true);

        _gunEquiped.transform.parent = null;
        _gunEquiped.transform.position = new Vector3(
            transform.position.x, 1.5f, transform.position.z);
        _gunEquiped.transform.localRotation = Quaternion.Euler(
            0f, 90f, 0f);
        SceneManager.MoveGameObjectToScene(_gunEquiped, SceneManager.GetActiveScene());

        if (CurrentGunShoot.AmmoCurrent <= 0)
            Destroy(_gunEquiped);

        CurrentGunShoot = null;
        _gunEquiped = null;

        FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/drop_weapon");  //Som dropar arma

        //Reequipar o bastão
        Bat batScript = BatClone.GetComponent<Bat>();
        BatClone.SetActive(true);

        UiHUD.Instance.HudChangeWeapon(
            batScript.WeaponData.MaxAmmo, 
            batScript.WeaponData.HudImage, 
            UiHUD.Instance.HudWeaponImageDefault );

        _animator.SetBool(IS_WITH_BAT, true);
        _weaponEquiped = WeaponEquiped.BAT;
        HasGun = false;
    }

    private void SetGunToHand(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        gun.transform.SetParent(_handBone.transform);
        gun.transform.localPosition = inHandPosition;
        gun.transform.localEulerAngles = inHandRotation;
    }

}