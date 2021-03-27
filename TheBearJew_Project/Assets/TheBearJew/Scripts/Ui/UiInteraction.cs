using UnityEngine;
using UnityEngine.UI;

public class UiInteraction : MonoBehaviour
{
    public static UiInteraction instance;

    [SerializeField] GameObject _uiInteraction;
    [SerializeField] Image _hudGunImage;

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

}
