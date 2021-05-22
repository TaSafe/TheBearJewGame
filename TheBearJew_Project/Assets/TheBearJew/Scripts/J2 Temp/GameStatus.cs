using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance { get; private set; }

    #region PISO 1F Variables

    [SerializeField] private bool _HasPlayedCutsceneOne;
    public bool HasEnteredSewer { get; set; }
    public bool HasEnemyAlive { get; set; } = true;

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

        //Debug.Log("Scene loaded: " + SceneManager.GetActiveScene().name);
    }

    private void Piso1FChecks()
    {
        if (!_HasPlayedCutsceneOne)
        {
            VideoController.Instance?.VideoActivate();
            _HasPlayedCutsceneOne = true;
        }

        if (!HasEnemyAlive)
            ManagerPiso1F.Instance?.LevelEndUpdate();
    }

    #region Piso_0F
    private void Piso0FEsgotoChecks()
    {
        Transform spawn = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        PlayerInput.Instance.PlayerBehaviour.SetPlayerPosition(spawn.position);
    }
    #endregion

    private void TuneisSubterraneosChechks() { }
}