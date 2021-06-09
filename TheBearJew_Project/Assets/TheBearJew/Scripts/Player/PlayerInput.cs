using UnityEngine;

//HACK: Temporariamente transformando essa classe no singleton do Player
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    
    public PlayerWeaponHandler PlayerWeaponHandler { get; private set; }
    public PlayerBehaviour PlayerBehaviour { get; private set; }
    public Inventory Inventory { get; private set; }
    public bool IsAllInputsEnable { get; set; } = true; //HACK: pra parar tudo temporariamente

    private Aim _aim;
    private Movement _movement;
    private PlayerInteraction _playerInteraction;
    private PlayerRoll _playerRoll;
    private bool inputEnabled = true;
    private bool videoInputEnabled;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _aim = GetComponent<Aim>();
        _movement = GetComponent<Movement>();
        _playerInteraction = GetComponentInChildren<PlayerInteraction>();
        _playerRoll = GetComponent<PlayerRoll>();
        PlayerWeaponHandler = GetComponent<PlayerWeaponHandler>();
        Inventory = GetComponent<Inventory>();
        PlayerBehaviour = GetComponent<PlayerBehaviour>();
    }

    private void Update() => PlayerInputs();

    private void PlayerInputs()
    {
        //Vídeo
        if (videoInputEnabled)
        {
            if (Input.GetKey(KeyCode.Space))
                VideoController.Instance.JumpVideo();
            else
                VideoController.Instance.JumpVideoReset();

            return;
        }

        //Pausa
        if (Input.GetKeyDown(KeyCode.Escape))
            UiHUD.Instance.Pause();

        if (!IsAllInputsEnable) return;

        //Interação
        if (_playerInteraction.CurrentInteraction != null)
        {
            if (Input.GetMouseButtonDown(1) && _playerInteraction.CurrentInteraction.MyType == IInteraction.InteractionType.GUN)
                _playerInteraction.WeaponHandlerInteraction(PlayerWeaponHandler);

            if (Input.GetKeyDown(KeyCode.E) && _playerInteraction.CurrentInteraction.MyType == IInteraction.InteractionType.GENERAL)
                _playerInteraction.WeaponHandlerInteraction(PlayerWeaponHandler);
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
                _playerInteraction.WeaponHandlerInteraction(PlayerWeaponHandler);
        }

        if (!inputEnabled) return;

        //In-game
        _aim.Aiming();

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        _movement.Move(xInput, yInput);

        if (Input.GetMouseButton(0))
            PlayerWeaponHandler.Attack(false);

        if (Input.GetMouseButtonDown(0))
                PlayerWeaponHandler.Attack(true);
        
        if (Input.GetKeyDown(KeyCode.Q))
            PlayerWeaponHandler.SwitchWeapons();

        if (Input.GetKeyDown(KeyCode.Space))
            _playerRoll.ActivateRoll = true;

        if (Input.GetKeyUp(KeyCode.Space))
            _playerRoll.ActivateRoll = false;
    }

    public void EnableInput() => inputEnabled = true;
    public void DisableInput() => inputEnabled = false;

    public void SetVideo(bool state) => videoInputEnabled = state;
}