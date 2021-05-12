using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance { get; private set; }

    #region PISO 1F Variables

    [SerializeField] private bool _cutsceneOne;
    [SerializeField] private Collider _Crowbar;
    [SerializeField] private GameObject _CrowbarParticle;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    public bool HasEnteredSewer;

    private bool HasEnemyAlive;
    private bool _crowbarCollected;

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() => SceneManager.sceneLoaded += SceneLoaded;

    private void OnDisable() => SceneManager.sceneLoaded -= SceneLoaded;

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Piso_1F":
                Piso1FChecks();
                break;
            case "Piso_0F_Esgoto":
                Piso0FEsgotoChecks();
                break;
            case "Tuneis_Subterraneos":
                TuneisSubterraneosChechks();
                break;
        }

        Debug.Log("Scene loaded: " + SceneManager.GetActiveScene().name);
    }

    #region Piso_1F
    private void Piso1FChecks()
    {
        if (!_cutsceneOne)
        {
            VideoController.instance.VideoActivate();
            _cutsceneOne = true;
        }

        if (_crowbarCollected)
        {
            _Crowbar.gameObject.SetActive(false);
            foreach (GameObject enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
    public void EnemyDeadPiso1F(GameObject item)
    {
        if (_crowbarCollected) return;

        enemies.Remove(item);

        if (enemies.Count < 0) HasEnemyAlive = false;

        if (!HasEnemyAlive)
        {
            _Crowbar.enabled = true;
            _CrowbarParticle.SetActive(true);
        }
    }
    public void Piso1FKeyCollected() => _crowbarCollected = true;
    #endregion

    private void Piso0FEsgotoChecks()
    {
        Transform spawn = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        PlayerInput.Instance.PlayerBehaviour.SetPlayerPosition(spawn.position);
    }

    private void TuneisSubterraneosChechks() { }
}