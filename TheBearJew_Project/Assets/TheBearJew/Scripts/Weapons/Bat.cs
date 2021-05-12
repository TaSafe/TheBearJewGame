using System.Collections;
using UnityEngine;

public class Bat : Weapon
{
    [Range(0f, 1f)] [SerializeField] private float _attackStartTime;
    [Range(0f, 1f)] [SerializeField] private float _attackRadius;

    private GameObject _muzzle;
    private Animator _animator;

    private void Start()
    {
        var playerRef = GameObject.FindGameObjectWithTag("Player"); //FIXME: mudar para instancia do player
        _muzzle = playerRef.GetComponent<PlayerWeaponHandler>().Muzzle;
        _animator = playerRef.GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        _animator.SetTrigger("batSlash");
        StartCoroutine(Attacking(_attackStartTime));
    }

    private IEnumerator Attacking(float attackTime)
    {
        var time = attackTime + Time.deltaTime;

        while (time > 0f)
        {
            if (time < .3f) //Sound
                FMODUnity.RuntimeManager.PlayOneShot(WeaponData.SoundShoot); 

            time -= Time.deltaTime;
            yield return null;
        }

        //Ataque
        Collider[] colliders = Physics.OverlapSphere(_muzzle.transform.position, _attackRadius);

        if (colliders == null) yield break;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player")) continue;

            IDamage iDamage = collider.GetComponent<IDamage>();
            if (iDamage != null)
                iDamage.Damage(WeaponData.Damage, true);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
