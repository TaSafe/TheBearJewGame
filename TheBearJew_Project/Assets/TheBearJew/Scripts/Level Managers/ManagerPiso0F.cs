using System.Collections.Generic;
using UnityEngine;

public class ManagerPiso0F : MonoBehaviour
{
    public static ManagerPiso0F Instance { get; private set; }

    [SerializeField] private GameObject _key;

    [Header("Enemies")]
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

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
        {
            GameStatus.Instance.HasEnemyAlivePiso0F = false;
            _key.SetActive(true);
        }
    }

    public void LevelEndUpdate()
    {
        foreach (GameObject enemy in _enemies)
        {
            enemy.gameObject.SetActive(false);
        }

        if (!GameStatus.Instance.HasPickedUpTheKey)
        {
            _key.SetActive(true);
        }
    }

    public void KeyPickedUp() => GameStatus.Instance.HasPickedUpTheKey = true;
}
