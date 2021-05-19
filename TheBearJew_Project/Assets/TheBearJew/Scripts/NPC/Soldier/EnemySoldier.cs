using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldier : Enemy
{
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private List<GameObject> _patrolPoints = new List<GameObject>();

    private BTRoot behaviour;

    private void Start()
    {
        EnemyInit(_enemyData);

        behaviour = GetComponent<BTRoot>();

        BTSequence patrol = new BTSequence();
        patrol.children.Add( new NodeMoveToTarget(transform, GetComponent<NavMeshAgent>(), _patrolPoints) );

        behaviour.root = patrol;
        StartCoroutine(behaviour.Execute());
    }

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