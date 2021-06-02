using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossStatus : MonoBehaviour
{
    [SerializeField] private EnemyDataSO _enemyData;

    [Header("Attack Atributes")]
    [SerializeField] private Transform _muzzle;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _rangeFarAttack;

    [FMODUnity.EventRef] public string smgShootSound;
    public float smgFireRate = .45f;
    public float smgDamage = 15f;
    public float shootTime = 5f;
    public float shootCooldown = 3f;
    public List<Transform> reloadPoints = new List<Transform>();
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;

    public Transform Muzzle { get { return _muzzle; } }
    public GameObject Bullet { get { return _bulletPrefab; } }
    public float RangeFarAttack { get { return _rangeFarAttack; } }
    public LifeSystem LifeSystem { get; private set; }

    private void Start()
    {
        LifeSystem = new LifeSystem(_enemyData.Life);
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        BossUI.Instance.LifeBarSetMaxValue(_enemyData.Life);
    }
}
