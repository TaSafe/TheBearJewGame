using UnityEngine;
using Cinemachine;

public class CmFollowPlayer : MonoBehaviour
{
    private static CmFollowPlayer instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
}
