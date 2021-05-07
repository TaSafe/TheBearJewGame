using UnityEngine;

public class CameraDontDestroy : MonoBehaviour
{
    public static CameraDontDestroy instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
