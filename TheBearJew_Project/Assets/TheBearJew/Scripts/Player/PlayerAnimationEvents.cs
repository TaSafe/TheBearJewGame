using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Animator _animator;

    private void Start() => _animator = GetComponent<Animator>();

    public void AnimationBatHitReset() => _animator.SetInteger("hitBatAnimation", 0);

    public void BatAttack() => PlayerInput.Instance?.PlayerWeaponHandler.BatClone.GetComponent<Bat>().AttackFromAnimation();
}
