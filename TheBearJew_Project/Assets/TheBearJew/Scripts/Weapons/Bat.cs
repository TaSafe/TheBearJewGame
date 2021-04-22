using System.Collections;
using UnityEngine;

public class Bat : MonoBehaviour
{

    [SerializeField] private WeaponData _gunData;

    [Header("Posição ao ser pega pelo player")]
    public Vector3 inHandPos;
    public Vector3 inHandRot;

    public WeaponData GunData { get { return _gunData; } }

}
