using UnityEngine;
using Cinemachine;

public class CmFollowPlayer : MonoBehaviour
{
    void Awake()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<CinemachineVirtualCamera>().Follow = player;
    }
}
