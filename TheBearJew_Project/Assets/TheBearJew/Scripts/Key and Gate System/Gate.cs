using UnityEngine;

public abstract class Gate : MonoBehaviour, IInteraction
{
    [SerializeField] private string _keyName;

    public IInteraction.InteractionType MyType { get; set; } = IInteraction.InteractionType.GENERAL;

    public abstract void GateActions();

    public abstract void Interaction();

    public bool CheckKeyInPlayerInventary()
    {
        if (Player.Instance.Inventory.HasItemOfType<Key>(out Key key))
        {
            if (key.KeyName == _keyName)
                return true;
        }
        
        return false;
    }

    public void RemoveKeyFromPlayerInventary()
    {
        Key key;
        if (Player.Instance.Inventory.HasItemOfType<Key>(out key))
        {
            Player.Instance.Inventory.RemoveItem(key.gameObject);
            key.gameObject.transform.parent = null;
            UiHUD.Instance.UIItemRemove(key.KeyHudImage);
            Destroy(key.gameObject);
        }
    }

}