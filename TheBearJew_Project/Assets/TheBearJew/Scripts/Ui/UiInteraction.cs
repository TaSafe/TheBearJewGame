using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInteraction : MonoBehaviour
{
    public static UiInteraction instance;

    [SerializeField] GameObject _uiInteraction;
    [SerializeField] Image _hudWeaponImageActive;
    [SerializeField] Image _hudWeaponImageInactive;
    [SerializeField] TMP_Text _hudGunAmmo;
    [SerializeField] Slider _playerLifeBar;

    public enum HUDWeapon { none, active, inactive }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowUi(bool value)
    {
        _uiInteraction.SetActive(value);
    }

    public void HudWeaponImage(Sprite sprite, HUDWeapon activeState = HUDWeapon.none)
    {
        if (activeState == HUDWeapon.none || activeState == HUDWeapon.active)
            _hudWeaponImageActive.sprite = sprite;
        else
            _hudWeaponImageInactive.sprite = sprite;
    }

    /// <summary>
    /// Atualiza o valor da munição na HUD
    /// </summary>
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

    //Player
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

}
