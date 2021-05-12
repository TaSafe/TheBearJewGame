using UnityEngine;

//HACK: Temporariamente transformando essa classe no singleton do Player
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    public Inventory Inventory { get; private set; }
    public PlayerWeaponHandler PlayerWeaponHandler { get; private set; }
    public PlayerBehaviour PlayerBehaviour { get; private set; }
    public bool IsAllInputsEnable { get; set; } //HACK: pra parar tudo temporariamente
    
    private Aim _aim;
    private Movement _movement;
    private PlayerInteraction _playerInteraction;
    private bool isInputEnable = true;

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
        PlayerWeaponHandler = GetComponent<PlayerWeaponHandler>();
        Inventory = GetComponent<Inventory>();
        PlayerBehaviour = GetComponent<PlayerBehaviour>();

        IsAllInputsEnable = true;
    }

    private void Update() => PlayerInputs();

    private void PlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UiHUD.instance.Pause();

        if (!IsAllInputsEnable) return;

        if (Input.GetMouseButtonDown(1))
            _playerInteraction.Interact(PlayerWeaponHandler);

        if (!isInputEnable) return;

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

        //FIXME: Adicionar o input do espaço do PlayerRoll
    }

    public void EnableInput() => isInputEnable = true;
    public void DisableInput() => isInputEnable = false;
}