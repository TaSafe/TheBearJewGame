using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySoldier : Enemy
{
    [SerializeField] private EnemyDataSO _enemyData;

    [Header("Attack")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzle;
    private bool _shoot; //Talvez saia depois

    [Header("UI")]
    [SerializeField] private GameObject _lifeUi;
    private UiLifeEnemy _uiLife;

    [Header("Patrol Points")]
    [SerializeField] private bool _movingPatrol = true;
    [SerializeField] private List<Transform> _patrolPoints = new List<Transform>();

    [Header("Patrol Detections")]
    [SerializeField] private bool _isPursuing;
    [SerializeField] private float _detectionRadius = 8f;
    [SerializeField] private float _detectionConeAngle = 60f;
    [SerializeField] private float _attackRange = 5f;

    private BTRoot behaviour;
    
    private void Awake() => EnemyInit(_enemyData);

    private void Start()
    {
        GameObject uiLife = Instantiate(_lifeUi, _lifeUi.transform.position, Quaternion.Euler(new Vector3(45f, 0f, 0f)));
        _uiLife = uiLife.GetComponent<UiLifeEnemy>();
        _uiLife.SetValues(transform, _enemyData.Life);

        BehaviourTree();
    }

    private void Update() => PursueState = _isPursuing;

    public override void BehaviourTree()
    {
        behaviour = GetComponent<BTRoot>();

        BTSequence playerDetection = new BTSequence();
        playerDetection.children.Add( new NodePlayerNear(_detectionRadius) );
        playerDetection.children.Add( new NodeIsOnViewCone(_detectionConeAngle, _detectionRadius));
        playerDetection.children.Add( new NodeApproximationToPlayer(_attackRange, GetComponent<NavMeshAgent>()) );
        playerDetection.children.Add( new NodeAttack() );

        BTSequence patrol = new BTSequence();
        //O if é para facilitar durante o desenvolvimento, não afeta em nada a lógica
        if (_movingPatrol)
            patrol.children.Add(new NodePatrolMove(transform, GetComponent<NavMeshAgent>(), _patrolPoints));
        else
            patrol.children.Add(new NodePatrolMove(transform, GetComponent<NavMeshAgent>(), null));

        BTSelectorParallel enemySoldierBehaviour = new BTSelectorParallel();
        enemySoldierBehaviour.children.Add( playerDetection );
        enemySoldierBehaviour.children.Add( patrol );

        behaviour.root = enemySoldierBehaviour;
        StartCoroutine(behaviour.Execute());
    }

    public override void Damage(float damage, bool bat = false) 
    { 
        //print($"Damage: {damage}; Bat: {bat}");
        LifeStatus.RemoveLife(damage);
        _uiLife.ChangeValue(LifeStatus.CurrentLife);
        DeathCheck(bat);
    }

    protected override void DeathCheck(bool batAttack) 
    { 
        //print($"DeathCheck > Bat attack {batAttack}");
        if (LifeStatus.IsDead())
        {
            _uiLife.Destroy();
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