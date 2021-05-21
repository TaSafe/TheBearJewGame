using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldier : Enemy
{
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzle;

    [Header("Patrol Points")]
    [SerializeField] private bool _movingPatrol = true;
    [SerializeField] private List<Transform> _patrolPoints = new List<Transform>();

    [Header("Patrol Actions")]
    [SerializeField] private bool _isPursuing;
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private float _detectionConeAngle = 30f;

    private BTRoot behaviour;
    
    //Talvez saia depois
    private bool _shoot;

    private void Awake() => EnemyInit(_enemyData);

    private void Start() => BehaviourTree();

    private void Update() => PursueState = _isPursuing;

    public override void BehaviourTree()
    {
        behaviour = GetComponent<BTRoot>();

        BTSequence patrol = new BTSequence();
        patrol.children.Add(new NodeAttack());
        //patrol.children.Add( new NodeApproximationToPlayer(3f, GetComponent<NavMeshAgent>()) );

        //O if é para facilitar durante o desenvolvimento, não afeta em nada a lógica
        //if (_movingPatrol)
        //    patrol.children.Add( new NodePatrolMove(transform, GetComponent<NavMeshAgent>(), _patrolPoints) );
        //else
        //    patrol.children.Add( new NodePatrolMove(transform, GetComponent<NavMeshAgent>(), null) );

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

    public override void Attack()
    {
        Vector3 playerPosition = PlayerInput.Instance.transform.position;
        Vector3 toLook = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
        transform.LookAt(toLook);

        //shoot
        if (!_shoot)
            StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        Vector3 bulletSpawnPosition = new Vector3(transform.position.x, 1.5f, transform.position.z * .8f);
        Instantiate(_bullet, _muzzle.position, transform.rotation);
        _shoot = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Guns/enemy_shot"); //som tiro

        yield return new WaitForSeconds(.7f);

        _shoot = false;
    }
    
}