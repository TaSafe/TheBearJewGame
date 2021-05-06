using UnityEngine;

//HACK: Temporariamente transformando essa classe no singleton do Player
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public Inventory Inventory { get; private set; }

    private Aim _aim;
    private Movement _movement;
    private PlayerInteraction _playerInteraction;
    private PlayerWeaponHandler _playerWeaponHandler;

    private bool isInputEnable = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _aim = GetComponent<Aim>();
        _movement = GetComponent<Movement>();
        _playerInteraction = GetComponentInChildren<PlayerInteraction>();
        _playerWeaponHandler = GetComponent<PlayerWeaponHandler>();
        Inventory = GetComponent<Inventory>();
    }

    private void Update() => PlayerInputs();

    private void PlayerInputs()
    {
        if (Input.GetMouseButtonDown(1))
            _playerInteraction.Interact(_playerWeaponHandler);

        //HACK: Apenas para testar o funcionamento do vídeo
        if (Input.GetKeyDown(KeyCode.J))
            VideoController.instance?.VideoActivate();

        if (!isInputEnable) return;

        _aim.Aiming();

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        _movement.Move(xInput, yInput);

        if (Input.GetMouseButton(0))
            _playerWeaponHandler.Attack(false);

        if (Input.GetMouseButtonDown(0))
            _playerWeaponHandler.Attack(true);

        if (Input.GetKeyDown(KeyCode.Q))
            _playerWeaponHandler.SwitchWeapons();

        //FIXME: Adicionar o input do espaço do PlayerRoll
    }

    public void EnableInput() => isInputEnable = true;
    public void DisableInput() => isInputEnable = false;
}