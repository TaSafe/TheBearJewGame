using UnityEngine;

public class Bat : Weapon
{
    [Range(0f, 1f)] [SerializeField] private float _attackRadius;

    private GameObject _muzzle;
    private Animator _animator;

    private void Start()
    {
        _muzzle = Player.Instance.PlayerWeaponHandler.Muzzle;
        _animator = Player.Instance.GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        if (_animator.GetInteger("hitBatAnimation") == 0)
        {
            _animator.speed += _animator.speed * WeaponData.FireRate;
            int animationClip = Random.Range(1, 5);
            _animator.SetInteger("hitBatAnimation", animationClip);
            FMODUnity.RuntimeManager.PlayOneShot(WeaponData.SoundShoot);
        }
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
}