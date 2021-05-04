using UnityEngine;

public class Gate
{
    private string _keyName;

    public Gate(string keyName) => _keyName = keyName;

    public bool CheckKey(string keyName)
    {
        if (keyName == _keyName)
            return true;
        else
            return false;
    }
}