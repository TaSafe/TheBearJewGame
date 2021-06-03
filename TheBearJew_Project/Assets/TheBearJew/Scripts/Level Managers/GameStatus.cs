using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    public static GameStatus Instance { get; private set; }

    [SerializeField] private bool _HasPlayedCutsceneOne;
    [SerializeField] private bool _HasPlayedCutsceneTwo;
    [SerializeField] private bool _HasPlayedCutsceneThree;
    [SerializeField] private string _endScene;

    #region PISO 1F Variables
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
            case "Subida_Para_a_Abadia":
                 SubidaChecks();
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
        PlayerInput.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene().position);

        if (!_HasPlayedCutsceneTwo)
        {
            VideoController.Instance?.VideoActivate();
            _HasPlayedCutsceneTwo = true;
        }
    }

    private void SubidaChecks()
    {
        PlayerInput.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene().position);

        if (!_HasPlayedCutsceneThree)
        {
            VideoController.Instance?.VideoActivate();
            _HasPlayedCutsceneThree = true;
        }
    }

    public void EndGame()
    {
        SceneManager.MoveGameObjectToScene(PlayerInput.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(CameraDontDestroy.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        VideoController.Instance.OnVideoEnd?.RemoveListener(EndGame);
        SceneManager.LoadScene(_endScene);
    }

}