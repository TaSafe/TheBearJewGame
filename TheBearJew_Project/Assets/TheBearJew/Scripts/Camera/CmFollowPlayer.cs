using UnityEngine;
using Cinemachine;

public class CmFollowPlayer : MonoBehaviour
{
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
}
