using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBullets : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bulletsImage;

    [SerializeField]
    private GameObject[] bulletsBackground;

    private WeaponData _weapon;
    private GunShoot _ammo;
    void Start()
    {
        _weapon = PlayerInput.Instance.PlayerWeaponHandler.CurrentGunShoot.GunBehaviour.WeaponData;
        _ammo = PlayerInput.Instance.PlayerWeaponHandler.CurrentGunShoot;
       
    }

    
    void Update()
    {
        CheckifHasWeapon();
        Debug.Log(_weapon.Name);
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
        else
        {
            ChangeImages();
        }
    }

    private void ChangeImages()
    {
        Debug.Log(_weapon.Name);
       if(_weapon.Name == "Pistol")  //maxAmmo 30
        {
            
            for(int i = 0; i < 30; i++)
            {
                bulletsBackground[i].SetActive(true);
            }

            for(int i = 39; i > 29; i--)
            {
                bulletsBackground[i].SetActive(false);
                bulletsImage[i].SetActive(false);
            }
   
        }
       else if (_weapon.Name == "Submachine") //maxAmmo 40
        {
            for (int i = 0; i < 40; i++)
            {
                bulletsBackground[i].SetActive(true);
            }
        }



        for (int u = 0; u < _ammo.AmmoCurrent; u++)
        {
            bulletsImage[u].SetActive(true);
        }

    }

    public void DesativaImage()
    {
        int ammo = (int) _ammo.AmmoCurrent;

        if (bulletsImage[ammo].activeSelf)
        {
            for(int i = (int) _weapon.MaxAmmo -1; i > _ammo.AmmoCurrent - 1; i--)
            {
                bulletsImage[i].SetActive(false);
            }
        }
    }
}
