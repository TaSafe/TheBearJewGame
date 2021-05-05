using UnityEngine;

public abstract class Gate : MonoBehaviour
{
    [SerializeField] private GameObject _key;

    public abstract void GateActions();

    public bool CheckKey()
    {
        if (PlayerInput.instance.Inventory.ContainItem(_key))
        {
            gameObject.SetActive(false);
            return true;
        }
        else
            return false;
    }

}