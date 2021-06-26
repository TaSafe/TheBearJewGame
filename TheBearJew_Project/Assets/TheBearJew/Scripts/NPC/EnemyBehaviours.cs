using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviours : MonoBehaviour
{
    [SerializeField] private Behaviours _currentBehaviour;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _rangePlayerFollow;
    [SerializeField] private float _rangePlayerAttack;
    [SerializeField] private LayerMask _ignoreLayer;
    
    [Header("Gun Stuff")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float shootDamage = 20f;
    
    private enum Behaviours { patrol, follow, attack }
    private NavMeshAgent _navMeshAgent;
    private int _currentPatrolPoint;
    private Transform _playerPosition;
    private float _elevateRaycastStartPoint = 1.5f;
    private bool shoot;
    private Animator _animator;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerPosition = Player.Instance.gameObject.transform;
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (_navMeshAgent.velocity != Vector3.zero)
        {
            if (!_animator.GetBool("isMoving"))
                _animator.SetBool("isMoving", true);
        }
        else
        {
            if (_animator.GetBool("isMoving"))
                _animator.SetBool("isMoving", false);
        }

        switch (_currentBehaviour)
        {
            case Behaviours.patrol:
                Patrol();
                break;
            case Behaviours.follow:
                Follow();
                break;
            case Behaviours.attack:
                Attack();
                break;
        }
    }

    private void Patrol()
    {
        if (_navMeshAgent.remainingDistance < 2f) _currentPatrolPoint++;

        if (_currentPatrolPoint > _patrolPoints.Length - 1) _currentPatrolPoint = 0;

        _navMeshAgent.SetDestination(_patrolPoints[_currentPatrolPoint].position);

        if (CheckPlayerNear(_rangePlayerFollow)) ChangeBehaviour(Behaviours.follow);
    }

    private void Follow()
    {
        _navMeshAgent.SetDestination(_playerPosition.position);

        if (CheckPlayerNear(_rangePlayerAttack))
        {
            ChangeBehaviour(Behaviours.attack);
            _navMeshAgent.SetDestination(transform.position);
        }
        else if (!CheckPlayerNear(_rangePlayerFollow)) ChangeBehaviour(Behaviours.patrol);
    }

    private void Attack()
    {
        Vector3 toLook = new Vector3(_playerPosition.position.x, transform.position.y, _playerPosition.position.z);
        transform.LookAt(toLook);
        
        //shoot
        if (!shoot)
            StartCoroutine(Shoot());

        if (!CheckPlayerNear(_rangePlayerAttack)) ChangeBehaviour(Behaviours.follow);
    }


    private IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(_bullet, _muzzle.position, _muzzle.rotation);
        bullet.GetComponent<Bullet>().SetBullet(shootDamage, gameObject.name);
        shoot = true;
        //som tiro
        FMODUnity.RuntimeManager.PlayOneShot("event:/Guns/enemy_shot");
        yield return new WaitForSeconds(.8f);

        shoot = false;
    }

    private void ChangeBehaviour(Behaviours newBehave)
    {
        _currentBehaviour = newBehave;
    }

    private bool CheckPlayerNear(float range)
    {
        if (Vector3.Distance(transform.position, _playerPosition.position) < range)
        {
            Vector3 position = new Vector3(
                transform.position.x, 
                transform.position.y + _elevateRaycastStartPoint, 
                transform.position.z);

            Vector3 position2 = new Vector3(
                _playerPosition.position.x, 
                _playerPosition.position.y + _elevateRaycastStartPoint, 
                _playerPosition.position.z);

            Vector3 direction = position2 - position;

            Ray ray = new Ray(position, direction);

            Debug.DrawRay(ray.origin, ray.direction * 25f, Color.green);

            if (Physics.Raycast(ray, out RaycastHit hit, _ignoreLayer))
            {
                if (hit.collider.CompareTag("Player"))
                    return true;
            }
        }
        
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _rangePlayerFollow);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangePlayerAttack);
    }

}
