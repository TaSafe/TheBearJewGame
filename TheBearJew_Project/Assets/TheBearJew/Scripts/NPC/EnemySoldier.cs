using System.Collections;
using UnityEngine;

public class EnemySoldier : Enemy
{
    [SerializeField] private EnemyDataSO _enemyData;

    private void Start() => EnemyInit(_enemyData);

    public override void Damage(float damage, bool bat = false) 
    { 
        print($"Damage: {damage}; Bat: {bat}");
        LifeStatus.RemoveLife(damage);
        DeathCheck(bat);
    }

    protected override void DeathCheck(bool batAttack) 
    { 
        print($"DeathCheck > Bat attack {batAttack}");
        if (LifeStatus.IsDead())
        {
            print("Morreu");
            Destroy(gameObject);
        }
    }

}