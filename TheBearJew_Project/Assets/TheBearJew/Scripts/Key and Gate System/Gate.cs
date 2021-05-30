using UnityEngine;

public abstract class Gate : MonoBehaviour, IInteraction
{
    [SerializeField] private string _keyName;

    public abstract void GateActions();

    public abstract void Interaction();

    public bool CheckKeyInPlayerInventary()
    {
        if (PlayerInput.Instance.Inventory.HasItemOfType<Key>(out Key key))
        {
            if (key.KeyName == _keyName)
                return true;
        }
        
        return false;
    }

    public void RemoveKeyFromPlayerInventary()
    {
        Key key;
        if (PlayerInput.Instance.Inventory.HasItemOfType<Key>(out key))
        {
            PlayerInput.Instance.Inventory.RemoveItem(key.gameObject);
            key.gameObject.transform.parent = null;
            UiHUD.Instance.UIItemRemove(key.KeyHudImage);
            Destroy(key.gameObject);
        }
    }

}