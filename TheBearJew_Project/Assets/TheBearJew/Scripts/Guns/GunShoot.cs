using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunShoot : MonoBehaviour
{

    [SerializeField] private GameObject _vfxMuzzleFlash;
    [SerializeField] private GameObject _vfxHit;

    private GunBehaviour _gunBehaviour;
    public float AmmoCurrent { get; private set; }
    
    void Start()
    {
        _gunBehaviour = GetComponent<GunBehaviour>();
        AmmoCurrent = _gunBehaviour.GunData.MaxAmmo;
    }

    //GAMBIARRA
    bool _firstShoot = true;
    public void MakeShoot()
    {
        if (_firstShoot)
            StartCoroutine(ShootRate());
    }

    //Aqui executa o tiro
    public IEnumerator ShootRate()
    {
        Shoot(_gunBehaviour.GunData.Damage);
        _firstShoot = false;
        yield return new WaitForSeconds(_gunBehaviour.GunData.FireRate);
        _firstShoot = true;
    }

    private void Shoot(float damage)
    {
        if (AmmoCurrent <= 0)
        {
            //Debug.Log("Sem munição");
            //Som sem munição
            FMODUnity.RuntimeManager.PlayOneShot(_gunBehaviour.GunData.SoundNoAmmo);

            return;
        }

        var muzzleFlash = Instantiate(_vfxMuzzleFlash, _gunBehaviour.Muzzle.position, _gunBehaviour.Muzzle.rotation);
        muzzleFlash.transform.SetParent(_gunBehaviour.transform);   //para que o flash siga o movimento da arma

        if (Physics.Raycast(_gunBehaviour.Muzzle.position, _gunBehaviour.Muzzle.forward, out var hitInfo, float.MaxValue))
        {
            Instantiate(_vfxHit, hitInfo.point, Quaternion.identity);

            if (hitInfo.collider.gameObject.GetComponent<IDamage>() != null)
            {
                hitInfo.collider.gameObject.GetComponent<IDamage>().Damage(damage);

                //Feedback sonoro hit no inimigo 
                FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/hit_enemy_layer");
            }
        }

        AmmoCurrent--;
        UiInteraction.instance.HudGunAmmo(AmmoCurrent);

        //Som do tiro genérico
        FMODUnity.RuntimeManager.PlayOneShot(_gunBehaviour.GunData.SoundShoot);
    }

}
