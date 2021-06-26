using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteraction
{
    [SerializeField] private string _keyName;
    [SerializeField] private Sprite _keyHudImage;
    [SerializeField] private UnityEvent OnCollect;

    public string KeyName { get { return _keyName; } }
    public Sprite KeyHudImage { get { return _keyHudImage; } }

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;

    public void Interaction()
    {
        if (!Player.Instance.Inventory.ContainItem(gameObject))
        {
            Player.Instance.Inventory.AddItem(gameObject);
            UiHUD.Instance.UIItemAdd(KeyHudImage);
        }

        gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(0f, 1000f, 0f);
        gameObject.transform.SetParent(Player.Instance.transform);
        OnCollect?.Invoke();
    }
}