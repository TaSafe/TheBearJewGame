using System.Collections;
using UnityEngine;

public abstract class Weapon
{
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private GameObject _vfxMuzzleFlash;
    [SerializeField] private GameObject _vfxHit;

    [Header("Posição ao ser pega pelo player")]
    [SerializeField] private Vector3 inHandPos;
    [SerializeField] private Vector3 inHandRot;

    public virtual WeaponData WeaponData() { return _weaponData; }
    public abstract float AmmoCurrent { get; set; }

    public abstract void Shoot();
}