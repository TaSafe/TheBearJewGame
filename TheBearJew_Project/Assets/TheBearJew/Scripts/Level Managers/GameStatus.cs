using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance { get; private set; }

    #region PISO 1F Variables

    [SerializeField] private bool _HasPlayedCutsceneOne;
    public bool HasEnteredSewer { get; set; }
    public bool HasOpenedEndGate { get; set; }
    public bool HasEnemyAlivePiso1F { get; set; } = true;

    #endregion

    #region PISO 0F ESGOTO Variables
    public bool HasEnemyAlivePiso0F { get; set; } = true;
    public bool HasPickedUpTheKey { get; set; }
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

    private Transform GetSpawnPointInScene() => GameObject.FindGameObjectWithTag("SpawnPoint").transform;


    /************************************/
    /////         CHECAGENS          /////
    /************************************/

    private void Piso1FChecks()
    {
        if (!_HasPlayedCutsceneOne)
        {
            VideoController.Instance?.VideoActivate();
            _HasPlayedCutsceneOne = true;
        }

        if (!HasEnemyAlivePiso1F)
            ManagerPiso1F.Instance?.LevelEndUpdate();

        if (HasEnteredSewer)
            PlayerInput.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene().position);
    }

    private void Piso0FEsgotoChecks()
    {
        PlayerInput.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene().position);

        if (!HasEnemyAlivePiso0F)
            ManagerPiso0F.Instance?.LevelEndUpdate();
    }

    private void TuneisSubterraneosChechks() 
    {

    }
}