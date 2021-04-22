using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Game Data/Weapon")]
public class WeaponData : ScriptableObject
{

    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _fireRate;
    [SerializeField] private Sprite _hudImage;
    [FMODUnity.EventRef] [SerializeField] private string _soundShoot;
    [FMODUnity.EventRef] [SerializeField] private string _soundNoAmmo;

    public string Name { get { return _name; } }
    public Sprite HudImage { get { return _hudImage; } }
    public int Damage { get { return _damage; } }
    public int MaxAmmo { get { return _maxAmmo; } }
    public float FireRate { get { return _fireRate; } }
    public string SoundShoot { get { return _soundShoot; } }
    public string SoundNoAmmo { get { return _soundNoAmmo; } }
}
