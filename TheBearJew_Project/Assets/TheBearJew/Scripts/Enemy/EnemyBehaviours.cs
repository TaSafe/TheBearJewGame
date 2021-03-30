using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviours : MonoBehaviour
{
    [SerializeField] private Behaviours _currentBehaviour;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _rangePlayerFollow;
    [SerializeField] private float _rangePlayerAttack;
    [Header("Gun Stuff")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bullet;
    
    private enum Behaviours { patrol, follow, attack }
    private NavMeshAgent _navMeshAgent;
    private int _currentPatrolPoint;
    private Transform _playerPosition;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
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
        Debug.Log("ATACANDO JOGADOR");
        var toLook = new Vector3(_playerPosition.position.x, transform.position.y, _playerPosition.position.z);
        transform.LookAt(toLook);
        
        //shoot
        if (!shoot)
            StartCoroutine(Shoot());

        if (!CheckPlayerNear(_rangePlayerAttack)) ChangeBehaviour(Behaviours.follow);
    }

    bool shoot;
    private IEnumerator Shoot()
    {
        Instantiate(_bullet, _muzzle.position, _muzzle.rotation);
        shoot = true;
        yield return new WaitForSeconds(.8f);

        shoot = false;
    }

    private void ChangeBehaviour(Behaviours newBehave)
    {
        _currentBehaviour = newBehave;
    }

    //CÓDIGO NOJENTO ABAIXO - PREGUIÇA DE FAZER DIREITO
    private bool CheckPlayerNear(float range)
    {
        if (Vector3.Distance(transform.position, _playerPosition.position) < range)
        {
            var pos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);
            var pos2 = new Vector3(_playerPosition.position.x, _playerPosition.position.y, _playerPosition.position.z);

            var direction = pos2 - pos;

            Ray ray = new Ray(pos, direction);

            Debug.DrawRay(ray.origin, ray.direction * 50, Color.green);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Player"))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
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
