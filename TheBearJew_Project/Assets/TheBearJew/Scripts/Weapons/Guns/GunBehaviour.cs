using UnityEngine;

public class GunBehaviour : MonoBehaviour, IInteraction
{
    [SerializeField] private WeaponData _gunData;
    public GameObject interactableVFX;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public WeaponData WeaponData { get { return _gunData; } }
    public Transform Muzzle { get; private set; }

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GUN;

    private void Start()
    {
        Muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform;
    }

    public void Interaction()
    {
        if (PlayerInput.Instance.PlayerWeaponHandler.HasGun)
        {
            if (PlayerInput.Instance.PlayerWeaponHandler._weaponEquiped == PlayerWeaponHandler.WeaponEquiped.BAT)
                PlayerInput.Instance.PlayerWeaponHandler.SwitchWeapons();

            PlayerInput.Instance.PlayerWeaponHandler.DropGun();
        }

        UiHUD.Instance.ShowIntereactionUI(false);
        GetComponent<Collider>().enabled = false;
        interactableVFX.SetActive(false);

        PlayerInput.Instance.PlayerWeaponHandler.EquipGun(gameObject, inHandPos, inHandRot);
    }
}