using UnityEngine;

public class Bat : Weapon
{
    //[Range(0f, 1f)] [SerializeField] private float _attackStartTime;  //ANTIGO ATAQUE
    [Range(0f, 1f)] [SerializeField] private float _attackRadius;

    private GameObject _muzzle;
    private Animator _animator;

    private void Start()
    {
        _muzzle = PlayerInput.Instance.PlayerWeaponHandler.Muzzle;
        _animator = PlayerInput.Instance.GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        if (_animator.GetInteger("hitBatAnimation") == 0)
        {
            _animator.speed += _animator.speed * WeaponData.FireRate;
            int animationClip = Random.Range(1, 5);
            _animator.SetInteger("hitBatAnimation", animationClip);
        }
        //StartCoroutine(Attacking(_attackStartTime));  //ANTIGO ATAQUE
    }

    public void AttackFromAnimation()
    {
        Collider[] colliders = Physics.OverlapSphere(_muzzle.transform.position, _attackRadius);

        if (colliders == null) return;

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Player")) continue;

            IDamage iDamage = collider.GetComponent<IDamage>();
            if (iDamage != null)
                iDamage.Damage(WeaponData.Damage, true);
        }
    }

    //ANTIGO ATAQUE
    //private void OnDisable()
    //{
    //    StopAllCoroutines();
    //}

    //private IEnumerator Attacking(float attackTime)
    //{
    //    float time = attackTime + Time.deltaTime;

    //    while (time > 0f)
    //    {
    //        if (time < .3f) //Sound
    //            FMODUnity.RuntimeManager.PlayOneShot(WeaponData.SoundShoot); 

    //        time -= Time.deltaTime;
    //        yield return null;
    //    }

    //    //Ataque
    //    Collider[] colliders = Physics.OverlapSphere(_muzzle.transform.position, _attackRadius);

    //    if (colliders == null) yield break;

    //    foreach (var collider in colliders)
    //    {
    //        if (collider.CompareTag("Player")) continue;

    //        IDamage iDamage = collider.GetComponent<IDamage>();
    //        if (iDamage != null)
    //            iDamage.Damage(WeaponData.Damage, true);
    //    }
    //}
}
