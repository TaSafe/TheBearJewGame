using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public GameObject batPrefab;

    public bool HasGun { get; private set; }
    private GameObject _gunEquiped;
    private GameObject bat;

    private void Start()
    {
        bat = Instantiate(batPrefab);
        
        Bat batScript = bat.GetComponent<Bat>();
        EquipGun(bat, batScript.inHandPos, batScript.inHandRot);
        UiInteraction.instance.GunHudImage(batScript.GunData.HudImage);
        UiInteraction.instance.HudGunAmmo(batScript.GunData.MaxAmmo);
    }

    public void EquipGun(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        if (HasGun) return;

        if (gun == bat)
        {
            SetGunToHand(gun, inHandPosition, inHandRotation);
        }
        else
        {
            _gunEquiped = gun;
            bat.SetActive(false);
            SetGunToHand(_gunEquiped, inHandPosition, inHandRotation);

            //Som pegar arma
            FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/pick_weapon");
            
            HasGun = true;
        }
    }

    public void DropGun()
    {
        _gunEquiped.GetComponent<Collider>().enabled = true;
        _gunEquiped.transform.parent = null;
        _gunEquiped = null;

        Bat batScript = bat.GetComponent<Bat>();
        bat.SetActive(true);

        UiInteraction.instance.GunHudImage(batScript.GunData.HudImage);   //MUDA A ARMA EXIBIDA NO HUD
        UiInteraction.instance.HudGunAmmo(batScript.GunData.MaxAmmo);   //MUDA A ARMA EXIBIDA NO HUD

        //Som dropar arma
        FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/drop_weapon");

        HasGun = false;
    }

    public void Attack()
    {
        _gunEquiped?.GetComponent<GunShoot>().MakeShoot();
    }

    private void SetGunToHand(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        var child = GetComponentsInChildren<Transform>();
        GameObject neededChild = null;

        for (int i = 0; i < child.Length; i++)
        {
            if (child[i].Find("swat:RightHand") != null)
            {
                neededChild = child[i].gameObject;
                break;
            }
        }

        if (neededChild != null)
        {
            gun.transform.SetParent(neededChild.transform);
            gun.transform.localPosition = inHandPosition;
            gun.transform.localEulerAngles = inHandRotation;
        }
    }
}
