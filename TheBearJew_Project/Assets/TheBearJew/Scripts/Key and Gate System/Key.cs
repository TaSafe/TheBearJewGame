using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteraction
{
    [SerializeField] private string _keyName;
    [SerializeField] private Sprite _keyHudImage;
    [SerializeField] private UnityEvent OnCollect;

    public string KeyName { get { return _keyName; } }
    public Sprite KeyHudImage { get { return _keyHudImage; } }

    public void Interaction()
    {
        if (!PlayerInput.Instance.Inventory.ContainItem(gameObject))
        {
            PlayerInput.Instance.Inventory.AddItem(gameObject);
            UiHUD.Instance.UIItemAdd(KeyHudImage);
        }

        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(0f, 1000f, 0f);
        gameObject.transform.SetParent(PlayerInput.Instance.transform);
        OnCollect?.Invoke();
    }
}