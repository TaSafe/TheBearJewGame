using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviours : MonoBehaviour
{
    [SerializeField] private GunData _gunData;

    public Sprite GetGunHudImage()
    {
        return _gunData.HudImage;
    }
}
