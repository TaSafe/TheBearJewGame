using System.Collections.Generic;
using UnityEngine;

public class ManagerTuneis : MonoBehaviour
{
    public static ManagerTuneis Instance { get; private set; }

    [SerializeField] private GameObject _key;
    [SerializeField] private bool _hasVideoPlayed;

    public bool IsDynamiteInPlace { get; set; }
    public bool HasBlockExploded { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (!_hasVideoPlayed)
        {
            VideoController.Instance?.VideoActivate();
            _hasVideoPlayed = true;
        }
    }

    public void KeyEnemyDied()
    {
        _key.SetActive(true);
    }

}
