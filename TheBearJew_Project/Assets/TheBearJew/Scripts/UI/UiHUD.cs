using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiHUD : MonoBehaviour
{
    public static UiHUD instance;

    [SerializeField] GameObject _groupInteractionUI;
    [SerializeField] Image _hudWeaponImageActive;
    [SerializeField] Image _hudWeaponImageInactive;
    [SerializeField] Sprite _hudWeaponImageDefault;
    [SerializeField] TMP_Text _hudGunAmmo;
    [SerializeField] Slider _playerLifeBar;

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