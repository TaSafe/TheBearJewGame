using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "ScriptableObjects/Gun")]
public class GunData : ScriptableObject
{

    [SerializeField] private string _name;
    [SerializeField] private Sprite _hudImage;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _fireRate;
    [SerializeField] private ProjectileRange _range;

    [HideInInspector] public enum ProjectileRange { close, mid, far }

    [SerializeField] public string Name { get { return _name; } }
    public Sprite HudImage { get { return _hudImage; } }
    public int Damage { get { return _damage; } }
    public int MaxAmmo { get { return _maxAmmo; } }
    public float FireRate { get { return _fireRate; } }
    public ProjectileRange Range { get { return _range; } }

}
