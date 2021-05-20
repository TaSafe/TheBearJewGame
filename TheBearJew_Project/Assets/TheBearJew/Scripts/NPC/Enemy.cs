using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamage
{
    protected LifeSystem LifeStatus { get; private set; }
    public bool PursueState { get; set; }

    protected void EnemyInit(EnemyDataSO enemyData) => LifeStatus = new LifeSystem(enemyData.Life);

    public abstract void Damage(float damage, bool bat = false);

    protected abstract void DeathCheck(bool batAttack);
}