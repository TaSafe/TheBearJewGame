using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPJDois : MonoBehaviour
{
    public static GameManagerPJDois Instance { get; private set; }

    public bool LevelCheck { get; set; }

    #region PISO 1F

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

        LevelCheck = false;
    }

    private void Update()
    {
        if (!LevelCheck)
        {
            if (SceneManager.GetActiveScene().name == "Piso_1F")
            {
                Piso1FChecks();
                LevelCheck = true;
            }
            else if (SceneManager.GetActiveScene().name == "Piso_0F_Esgoto")
            {
                Piso0FEsgotoChecks();
                LevelCheck = true;
            }
            else if (SceneManager.GetActiveScene().name == "Tuneis_Subterraneos")
            {
                TuneisSubterraneosChechks();
                LevelCheck = true;
            }

            Debug.Log(SceneManager.GetActiveScene().name);
        }
    }

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

    private void Piso0FEsgotoChecks()
    {
        Transform spawn = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        PlayerInput.Instance.PlayerBehaviour.SetPlayerPosition(spawn.position);
    }

    private void TuneisSubterraneosChechks() { }

}