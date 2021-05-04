using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    
    private List<GameObject> _itens = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(GameObject item) => _itens.Add(item);

    public void RemoveItem(GameObject item)
    {
        if (HasItem(item))
            _itens.Remove(item);
    }

    public bool HasItem(GameObject item)
    {
        if (_itens.Contains(item))
            return true;
        else
            return false;
    }
}