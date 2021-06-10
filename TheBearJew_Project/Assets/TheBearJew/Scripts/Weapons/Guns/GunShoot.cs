using System.Collections;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private Transform _muzzleFxPosition;
    [SerializeField] private GameObject _vfxMuzzleFlash;
    [SerializeField] private GameObject _vfxHit;
    [SerializeField] private GameObject _vfxBlood;
    [SerializeField] private LayerMask _ignoreLayer;

    public float AmmoCurrent { get; private set; }
    public GunBehaviour GunBehaviour { get; private set; }
    private UiBullets uiBullets;
    
    private bool _firstShoot = true; //GAMBIARRA
    
    void Start()
    {
        GunBehaviour = GetComponent<GunBehaviour>();
        AmmoCurrent = GunBehaviour.WeaponData.MaxAmmo;
    }

    private void Update() => Debug.DrawRay(GunBehaviour.Muzzle.position, GunBehaviour.Muzzle.forward * 30f, Color.red);
    
    public void MakeShoot()
    {
        if (_firstShoot)
            StartCoroutine(ShootRate());
    }

    //Aqui executa o tiro
    public IEnumerator ShootRate()
    {
        Shoot(GunBehaviour.WeaponData.Damage);
        _firstShoot = false;
        yield return new WaitForSeconds(GunBehaviour.WeaponData.FireRate);
        _firstShoot = true;
    }

    private void Shoot(float damage)
    {
        if (AmmoCurrent <= 0)
        {
            FMODUnity.RuntimeManager.PlayOneShot(GunBehaviour.WeaponData.SoundNoAmmo); //Som sem munição
            return;
        }

        AmmoCurrent--;
        
        GameObject muzzleFlash = Instantiate(_vfxMuzzleFlash, _muzzleFxPosition.position, _muzzleFxPosition.rotation);
        muzzleFlash.transform.SetParent(_muzzleFxPosition);   //para que o flash siga o movimento da arma

        FMODUnity.RuntimeManager.PlayOneShot(GunBehaviour.WeaponData.SoundShoot);

        UiHUD.Instance.HudWeaponAmmo(AmmoCurrent);
        //uiBullets.DesativaImage();
        

        //Detecção do Raycast do tiro
        Ray ray = new Ray(GunBehaviour.Muzzle.position, GunBehaviour.Muzzle.forward);
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