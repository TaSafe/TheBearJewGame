using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBullets : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bulletsImage;

    [SerializeField]
    private GameObject[] bulletsBackground;

    private PlayerWeaponHandler _weapon;
    void Start()
    {
       // _weapon = PlayerWeaponHandler.WeaponEquiped[_we
       
    }

    
    void Update()
    {
        
    }


    private void CheckifHasWeapon()
    {
        if (!PlayerInput.Instance.PlayerWeaponHandler.HasGun)
        {
            for (int i = 0; i < bulletsImage.Length; i++)
            {
                bulletsImage[i].SetActive(false);
                
            }

            for (int i = 0; i < bulletsBackground.Length; i++)
            {
                bulletsBackground[i].SetActive(false);
            }
        }
    }

    private void ChangeImages()
    {
       // if(_playerWeaponHandler._weaponEquiped == WeaponEquiped.GUN)
    }
}
