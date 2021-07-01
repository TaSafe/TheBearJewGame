using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Transform _muzzleFxPosition;
    [SerializeField] private GameObject _vfxBlood;
    [SerializeField] private LayerMask _ignoreLayer;

    private bool _firstShoot = true; //GAMBIARRA

    void Start() => WeaponInit();

    //private void Update() => Debug.DrawRay(Muzzle.position, Muzzle.forward * 30f, Color.red);

    public override void Attack()
    {
        if (_firstShoot)
            StartCoroutine(ShootRate());
    }

    //Aqui executa o tiro
    public IEnumerator ShootRate()
    {
        Shoot(WeaponData.Damage);
        _firstShoot = false;
        yield return new WaitForSeconds(WeaponData.FireRate);
        _firstShoot = true;
    }

    private void Shoot(float damage)
    {
        if (AmmoCurrent <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(WeaponData.SoundNoAmmo); //Som sem munição
            return;
        }

        AmmoReduce(1);

        GameObject muzzleFlash = Instantiate(_vfxMuzzleFlash, _muzzleFxPosition.position, _muzzleFxPosition.rotation);
        muzzleFlash.transform.SetParent(_muzzleFxPosition);   //para que o flash siga o movimento da arma

        FMODUnity.RuntimeManager.PlayOneShot(WeaponData.SoundShoot);

        UiHUD.Instance.HudWeaponAmmo(AmmoCurrent);

        //Detecção do Raycast do tiro
        Ray ray = new Ray(Muzzle.position, Muzzle.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 30f, _ignoreLayer))
        {
            if (hitInfo.collider.gameObject.GetComponent<IDamage>() != null)
            {
                hitInfo.collider.gameObject.GetComponent<IDamage>().Damage(damage);

                Instantiate(_vfxBlood, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));

                //Feedback sonoro hit no inimigo FIXME: mover para a classe que lida com o dano do inimigo
                FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/hit_enemy_layer");

                return;
            }

            Instantiate(_vfxHit, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _firstShoot = true;
    }

}