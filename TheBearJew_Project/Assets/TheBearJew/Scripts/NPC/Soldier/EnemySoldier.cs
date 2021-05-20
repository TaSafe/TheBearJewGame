using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldier : Enemy
{
    [SerializeField] private EnemyDataSO _enemyData;

    [SerializeField] private bool _movingPatrol = true;
    [SerializeField] private List<Transform> _patrolPoints = new List<Transform>();

    private BTRoot behaviour;

    private void Awake() => EnemyInit(_enemyData);

    private void OnEnable()
    {
        behaviour = GetComponent<BTRoot>();

        BTSequence patrol = new BTSequence();

        //O if é para facilitar durante o desenvolvimento, não afeta em nada a lógica
        if (_movingPatrol)
            patrol.children.Add( new NodePatrolMove(transform, GetComponent<NavMeshAgent>(), _patrolPoints) );
        else
            patrol.children.Add(new NodePatrolMove(transform, GetComponent<NavMeshAgent>(), null));

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