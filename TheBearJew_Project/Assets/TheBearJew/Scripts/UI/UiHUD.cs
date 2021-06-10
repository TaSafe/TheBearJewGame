using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiHUD : MonoBehaviour
{
    public static UiHUD Instance { get; private set; }
    public Sprite HudWeaponImageDefault { get { return _hudWeaponImageDefault; } }
    public UiRollCooldownProgress CooldownProgressBar { get; private set; }


    [SerializeField] private GameObject _groupInteractionMouseUI;
    [SerializeField] private GameObject _groupInteractionKeyEUI;
    [SerializeField] private GameObject _groupPauseMenu;
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Slider _playerLifeBar;
                     
    [Header("HUD Weapon")]
    [SerializeField] private Image _hudWeaponImageActive;
    [SerializeField] private Image _hudWeaponImageInactive;
    [SerializeField] private Sprite _hudWeaponImageDefault;
    [SerializeField] private TMP_Text _hudGunAmmo;

    [Header("Diálogo")]
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private TMP_Text _dialogueCharacater;
    [SerializeField] private TMP_Text _dialogueTxt;
    [SerializeField] private Image _dialogueCharacterImg;

    [Header("HUD Itens")]
    [SerializeField] private GameObject _hudItensGroup;
    
    private Image[] _hudItensSlots;
    private Color _inactiveHudWeaponAlphaOne;
    private Color _inactiveHudWeaponAlphaZero;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable() => CooldownProgressBar = FindObjectOfType<UiRollCooldownProgress>();

    private void Start()
    {
        _hudItensSlots = _hudItensGroup.GetComponentsInChildren<Image>();
        for (int i = 0; i < _hudItensSlots.Length; i++)
        {
            _hudItensSlots[i].enabled = false;
        }

        _inactiveHudWeaponAlphaOne = new Color(
                    _hudWeaponImageInactive.color.r,
                    _hudWeaponImageInactive.color.g,
                    _hudWeaponImageInactive.color.b,
                    1f);
        _inactiveHudWeaponAlphaZero = new Color(
                    _hudWeaponImageInactive.color.r,
                    _hudWeaponImageInactive.color.g,
                    _hudWeaponImageInactive.color.b,
                    0f);
    }

    #region ITEM
    public void UIItemAdd(Sprite sprite)
    {
        for (int i = 0; i < _hudItensSlots.Length; i++)
        {
            if (!_hudItensSlots[i].enabled)
            {
                _hudItensSlots[i].sprite = sprite;
                _hudItensSlots[i].enabled = true;
                break;
            }
        }
    }

    public void UIItemRemove(Sprite sprite)
    {
        for (int i = 0; i < _hudItensSlots.Length; i++)
        {
            if (_hudItensSlots[i].enabled && _hudItensSlots[i].sprite == sprite)
            {
                _hudItensSlots[i].sprite = HudWeaponImageDefault;
                _hudItensSlots[i].enabled = false;
                break;
            }
        }
    }

    public void ShowIntereactionUI(bool value, bool isMouse = false)
    {
        if (!value)
        {
            _groupInteractionMouseUI.SetActive(value);
            _groupInteractionKeyEUI.SetActive(value);

            return;
        }

        if (isMouse)
            _groupInteractionMouseUI.SetActive(value);
        else
            _groupInteractionKeyEUI.SetActive(value);
    }
    #endregion

    //FIXME: mover para uma classe própria a função Pause()
    public void Pause()
    {
        if (Time.timeScale > 0f)
        {
            PlayerInput.Instance.IsAllInputsEnable = false;
            _groupPauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _groupPauseMenu.SetActive(false);
            Time.timeScale = 1f;
            PlayerInput.Instance.IsAllInputsEnable = true;
        }
    }

    public void LoadLevel(string levelName) => SceneManager.LoadScene(levelName);

    public void QuitGame() => Application.Quit();

    #region HUD WEAPON
    public void HudWeaponAmmo(float currentAmmo)
    {
        if (currentAmmo == -1)
        {
            _hudGunAmmo.color = Color.white;
            _hudGunAmmo.SetText("\u221E");
        }
        else if (currentAmmo > 0)
        {
            _hudGunAmmo.color = Color.white;
            _hudGunAmmo.SetText($"{currentAmmo:000}");
        }
        else if (currentAmmo < -1)
        {
            _hudGunAmmo.color = Color.white;
            _hudGunAmmo.SetText("-");
        }
        else
        {
            _hudGunAmmo.color = Color.red;
            _hudGunAmmo.SetText("vazio");
        }
    }

    public void HudWeaponImage(Sprite sprite, bool activeState = true)
    {
        if (activeState)
            _hudWeaponImageActive.sprite = sprite;
        else
        {
            _hudWeaponImageInactive.sprite = sprite;
            if (sprite == HudWeaponImageDefault)
                _hudWeaponImageInactive.color = _inactiveHudWeaponAlphaZero;
            else
                _hudWeaponImageInactive.color = _inactiveHudWeaponAlphaOne;
        }
    }

    public void HudChangeWeapon(float currentAmmo, Sprite activeWeaponImage, Sprite inactiveWeaponImage)
    {
        HudWeaponAmmo(currentAmmo);
        HudWeaponImage(activeWeaponImage);
        HudWeaponImage(inactiveWeaponImage, false);
    }
    #endregion

    #region PLAYER
    public void SetLifeBar(float maxValue)
    {
        _playerLifeBar.maxValue = maxValue;
        _playerLifeBar.value = maxValue;
    }

    public void ChangeLifeBar(float newValue)
    {
        if (newValue < 0f)
            _playerLifeBar.value = 0f;
        else
            _playerLifeBar.value = newValue;
    }
    #endregion

    #region DIÁLOGO
    public void DialogueShow(bool activeState) => _dialogueUI.SetActive(activeState);

    public void DialogueChangeTexts(string character, string text, Sprite characterImage)
    {
        if (_dialogueCharacater.text != character)
        {
            _dialogueCharacater.SetText(character);
            _dialogueCharacterImg.sprite = characterImage;
        }
        
        if (_dialogueTxt.text != text)
            _dialogueTxt.SetText(text);
    }
    #endregion

    public void LoadingPanel(bool activeState) => _loadingPanel.SetActive(activeState);

    public void BackToMainMenu()
    {
        SceneManager.MoveGameObjectToScene(PlayerInput.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(CameraDontDestroy.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(GameStatus.Instance.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        SceneManager.LoadScene("MenuInicial");
    }

}