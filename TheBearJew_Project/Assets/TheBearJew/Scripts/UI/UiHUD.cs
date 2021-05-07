using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiHUD : MonoBehaviour
{
    public static UiHUD instance;

    [SerializeField] GameObject _groupInteractionUI;
    [SerializeField] GameObject _groupPauseMenu;
    [SerializeField] Slider _playerLifeBar;
    
    [Header("HUD Weapon")]
    [SerializeField] Image _hudWeaponImageActive;
    [SerializeField] Image _hudWeaponImageInactive;
    [SerializeField] Sprite _hudWeaponImageDefault;
    [SerializeField] TMP_Text _hudGunAmmo;

    [Header("Diálogo")]
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private TMP_Text _dialogueCharacater;
    [SerializeField] private TMP_Text _dialogueTxt;
    [SerializeField] private Image _dialogueCharacterImg;

    public Sprite HudWeaponImageDefault { get { return _hudWeaponImageDefault; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowIntereactionUI(bool value)
    {
        _groupInteractionUI.SetActive(value);
    }

    //FIXME: mover para uma classe própria a função Pause()
    public void Pause()
    {
        if (Time.timeScale > 0f)
        {
            PlayerInput.instance.IsAllInputsEnable = false;
            _groupPauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            _groupPauseMenu.SetActive(false);
            Time.timeScale = 1f;
            PlayerInput.instance.IsAllInputsEnable = true;
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
            _hudGunAmmo.SetText("infinito");
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
            _hudWeaponImageInactive.sprite = sprite;
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
}