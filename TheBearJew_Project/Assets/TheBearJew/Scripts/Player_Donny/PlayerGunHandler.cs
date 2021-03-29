using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunHandler : MonoBehaviour
{
    public bool HasGun { get; private set; }
    private GameObject _gunEquiped;

    public void EquipGun(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
    {
        if (HasGun) return;

        _gunEquiped = gun;

        GetGun(_gunEquiped, inHandPosition, inHandRotation);
        HasGun = true;
    }

    private void GetGun(GameObject gun, Vector3 inHandPosition, Vector3 inHandRotation)
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

    public void DropGun()
    {
        _gunEquiped.transform.parent = null;
        _gunEquiped.GetComponent<Collider>().enabled = true;
        UiInteraction.instance.GunHudImage(null);   //MUDA A ARMA EXIBIDA NO HUD
        HasGun = false;
    }

    public void FireGun()
    {
        _gunEquiped.GetComponent<GunShoot>().MakeShoot();
    }

}
