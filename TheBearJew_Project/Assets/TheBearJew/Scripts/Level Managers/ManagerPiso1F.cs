using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPiso1F : MonoBehaviour
{
    public static ManagerPiso1F Instance { get; private set; }

    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    [Header("Crowbar")]
    [SerializeField] private GameObject _Crowbar;
    [SerializeField] private GameObject _CrowbarParticle;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RemoveEnemiesFromList(GameObject enemy)
    {
        if (_enemies.Contains(enemy))
            _enemies.Remove(enemy);

        if (_enemies.Count <= 0)
            GameStatus.Instance.HasEnemyAlive = false;

        if (!GameStatus.Instance.HasEnemyAlive)
        {
            _Crowbar.GetComponent<Collider>().enabled = true;
            _CrowbarParticle.SetActive(true);
        }
    }

    public void LevelEndUpdate()
    {
        _Crowbar.SetActive(false);
        foreach (GameObject enemy in _enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

}
