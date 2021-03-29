using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiInteraction : MonoBehaviour
{
    public static UiInteraction instance;

    [SerializeField] GameObject _uiInteraction;
    [SerializeField] Image _hudGunImage;
    [SerializeField] TMP_Text _hudGunAmmo;

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

    public void GunHudImage(Sprite sprite)
    {
        _hudGunImage.sprite = sprite;
    }

    public void GunAmmo(float curretAmmo)
    {
        if (curretAmmo > 0)
        {
            _hudGunAmmo.color = Color.white;
            _hudGunAmmo.SetText($"{curretAmmo:000}");
        }
        else if (curretAmmo < 0)
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

}
