using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private GameObject _vfxMuzzleFlash;
    [SerializeField] private GameObject _vfxHit;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 _inHandPos;
    [SerializeField] private Vector3 _inHandRot;

    private float _ammoCurrent;

    public WeaponData WeaponData { get { return _weaponData; } }
    public float AmmoCurrent { get { return _ammoCurrent; } }
    public Vector3 ParentInHandPosition { get { return _inHandPos; } }
    public Vector3 ParentInHandRotation { get { return _inHandRot; } }

    public abstract void Attack();
}