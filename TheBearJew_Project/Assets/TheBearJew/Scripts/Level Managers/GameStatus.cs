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

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
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

        UiHUD.Instance?.LoadingPanel(false);
    }

    private Vector3 GetSpawnPointInScene() => GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;

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
            Player.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene());
    }

    private void Piso0FEsgotoChecks()
    {
        Player.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene());

        if (!HasEnemyAlivePiso0F)
            ManagerPiso0F.Instance?.LevelEndUpdate();
    }

    private void TuneisSubterraneosChechks() 
    {
        Player.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene());

        Player.Instance.PlayerBehaviour?.VisualChange();

        if (!_HasPlayedCutsceneTwo)
        {
            VideoController.Instance?.VideoActivate();
            _HasPlayedCutsceneTwo = true;
        }
    }

    private void SubidaChecks()
    {
        Player.Instance.PlayerBehaviour?.SetPlayerPosition(GetSpawnPointInScene());

        if (!_HasPlayedCutsceneThree)
        {
            VideoController.Instance?.VideoActivate();
            _HasPlayedCutsceneThree = true;
        }
    }

    public void EndGame()
    {
        SceneManager.MoveGameObjectToScene(Player.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(CameraDontDestroy.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(UiHUD.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        VideoController.Instance.OnVideoEnd?.RemoveListener(EndGame);
        SceneManager.LoadScene(_endScene);
    }
}