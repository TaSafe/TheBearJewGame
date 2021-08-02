using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public PlayerInput PlayerInput { get; private set; }
    public PlayerBehaviour PlayerBehaviour { get; private set; }
    public PlayerWeaponHandler PlayerWeaponHandler { get; private set; }
    public Aim Aim { get; private set; }
    public Movement Movement { get; private set; }
    public PlayerInteraction PlayerInteraction { get; private set; }
    public PlayerDash PlayerRoll { get; private set; }
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        PlayerInput = GetComponent<PlayerInput>();
        PlayerBehaviour = GetComponent<PlayerBehaviour>();
        PlayerWeaponHandler = GetComponent<PlayerWeaponHandler>();
        Aim = GetComponent<Aim>();
        Movement = GetComponent<Movement>();
        PlayerInteraction = GetComponentInChildren<PlayerInteraction>();
        PlayerRoll = GetComponent<PlayerDash>();
        Inventory = GetComponent<Inventory>();
    }
}