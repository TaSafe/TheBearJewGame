using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private string _keyName;

    public string KeyName { get { return _keyName; } }
}