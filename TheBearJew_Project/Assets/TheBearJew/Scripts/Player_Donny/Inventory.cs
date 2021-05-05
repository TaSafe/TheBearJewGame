using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> _itens = new List<GameObject>();

    public void AddItem(GameObject item) => _itens.Add(item);

    public void RemoveItem(GameObject item)
    {
        if (ContainItem(item))
            _itens.Remove(item);
    }

    public bool ContainItem(GameObject item)
    {
        if (_itens.Contains(item))
            return true;
        else
            return false;
    }

    public bool HasItemOfType<T>()
    {
        foreach (GameObject item in _itens)
        {
            if (item.gameObject.GetComponent<T>() != null)
                return true;
        }

        return false;
    }

}