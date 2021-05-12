using UnityEngine;

public class GunBehaviour : MonoBehaviour, IInteraction
{
    [SerializeField] private WeaponData _gunData;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public WeaponData WeaponData { get { return _gunData; } }
    public Transform Muzzle { get; private set; }

    private void Start() => Muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform;

    public void IdleInteraction() { }

    public void Interacting() { }

    public void Interaction()
    {
        if (PlayerInput.Instance.PlayerWeaponHandler.HasGun) return;

        UiHUD.instance.ShowIntereactionUI(false);
        GetComponent<Collider>().enabled = false;
        //FIXME: mudar o FindGameObjectWithTag("Player") para a instancia singleton
        GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerWeaponHandler>().EquipGun(gameObject, inHandPos, inHandRot);
    }
}
