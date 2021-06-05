using System.Collections;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private GameObject _vfxMuzzleFlash;
    [SerializeField] private GameObject _vfxHit;
    public float AmmoCurrent { get; private set; }

    private GunBehaviour _gunBehaviour;
    private bool _firstShoot = true; //GAMBIARRA
    
    void Start()
    {
        _gunBehaviour = GetComponent<GunBehaviour>();
        AmmoCurrent = _gunBehaviour.WeaponData.MaxAmmo;
    }

    private void Update() => Debug.DrawRay(_gunBehaviour.Muzzle.position, _gunBehaviour.Muzzle.forward * 50f, Color.red);
    
    public void MakeShoot()
    {
        if (_firstShoot)
            StartCoroutine(ShootRate());
    }

    //Aqui executa o tiro
    public IEnumerator ShootRate()
    {
        Shoot(_gunBehaviour.WeaponData.Damage);
        _firstShoot = false;
        yield return new WaitForSeconds(_gunBehaviour.WeaponData.FireRate);
        _firstShoot = true;
    }

    private void Shoot(float damage)
    {
        if (AmmoCurrent <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(_gunBehaviour.WeaponData.SoundNoAmmo); //Som sem munição
            return;
        }

        AmmoCurrent--;
        
        var muzzleFlash = Instantiate(_vfxMuzzleFlash, _gunBehaviour.Muzzle.position, _gunBehaviour.Muzzle.rotation);
        muzzleFlash.transform.SetParent(_gunBehaviour.transform);   //para que o flash siga o movimento da arma

        FMODUnity.RuntimeManager.PlayOneShot(_gunBehaviour.WeaponData.SoundShoot); //Som do tiro genérico

        UiHUD.Instance.HudWeaponAmmo(AmmoCurrent);

        //Detecção do Raycast do tiro
        if (Physics.Raycast(_gunBehaviour.Muzzle.position, _gunBehaviour.Muzzle.forward, out var hitInfo, float.MaxValue))
        {
            Instantiate(_vfxHit, hitInfo.point, Quaternion.identity);

            if (hitInfo.collider.gameObject.GetComponent<IDamage>() != null)
            {
                hitInfo.collider.gameObject.GetComponent<IDamage>().Damage(damage);

                //Feedback sonoro hit no inimigo FIXME: mover para a classe que lida com o dano do inimigo
                FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/hit_enemy_layer");
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _firstShoot = true;
    }
}