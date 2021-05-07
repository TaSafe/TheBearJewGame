using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// HACK: FIXME: ESSE SCRIPT TEVE A OREDEM DE EXECUÇÃO ALTERADA NO PROJECT SETTINGS
/// </summary>
public class GameManagerPJDois : MonoBehaviour
{
    public static GameManagerPJDois instance;

    private bool _levelCheck;

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
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!_levelCheck)
        {
            if (SceneManager.GetActiveScene().name == "Piso_1F")
                Piso1FChecks();
            else if (SceneManager.GetActiveScene().name == "Piso_0F_Esgoto")
                Piso0FEsgotoChecks();
            else if (SceneManager.GetActiveScene().name == "Tuneis_Subterraneos")
                TuneisSubterraneosChechks();

            _levelCheck = true;
        }
    }

    private void OnDisable() => _levelCheck = false;

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
            foreach (GameObject item in enemies)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    public void EnemyDeadPiso1F(GameObject item)
    {
        if (_crowbarCollected) return;

        enemies.Remove(item);

        if (enemies.Count < 0) HasEnemyAlive = false;
        Debug.Log(enemies.Count);

        if (!HasEnemyAlive)
        {
            _Crowbar.enabled = true;
            _CrowbarParticle.SetActive(true);
        }
    }
    public void Piso1FKeyCollected() => _crowbarCollected = true;

    private void Piso0FEsgotoChecks()
    {

    }

    private void TuneisSubterraneosChechks()
    {

    }

}