using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    private GunBehaviour _gunBehaviour;
    private float _ammoCurrent;
    
    void Start()
    {
        _gunBehaviour = GetComponent<GunBehaviour>();
        _ammoCurrent = _gunBehaviour.GunData.MaxAmmo;
    }

    //GAMBIARRA
    bool _firstShoot = true;
    public void MakeShoot()
    {
        if (_firstShoot)
            StartCoroutine(ShootRate());
    }

    public IEnumerator ShootRate()
    {
        Debug.Log("Tiro");
        Shoot(_gunBehaviour.GunData.Damage);
        _firstShoot = false;
        yield return new WaitForSeconds(_gunBehaviour.GunData.FireRate);
        _firstShoot = true;
    }

    private void Shoot(float damage)
    {
        if (_ammoCurrent < 0)
        {
            Debug.Log("Sem munição");
            return;
        }

        if (Physics.Raycast(_gunBehaviour.Muzzle.position, _gunBehaviour.Muzzle.forward, out var hitInfo, float.MaxValue))
        {
            if (hitInfo.collider.gameObject.GetComponent<IDamage>() != null)
                hitInfo.collider.gameObject.GetComponent<IDamage>().Damage(damage);
            //else
            //particula
        }

        _ammoCurrent--;
    }

}
