using UnityEngine;

public class UiInteraction : MonoBehaviour
{
    public static UiInteraction instance;

    [SerializeField] GameObject _uiInteraction;

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

}
