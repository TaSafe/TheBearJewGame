using UnityEngine;

public abstract class Gate : MonoBehaviour
{
    [SerializeField] private GameObject _key;

    public abstract void GateActions();

    public bool CheckKey()
    {
        if (PlayerInput.Instance.Inventory.ContainItem(_key))
        {
            return true;
        }
        else
            return false;
    }

}