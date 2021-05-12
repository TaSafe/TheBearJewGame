using UnityEngine;

public class CameraDontDestroy : MonoBehaviour
{
    public static CameraDontDestroy Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
