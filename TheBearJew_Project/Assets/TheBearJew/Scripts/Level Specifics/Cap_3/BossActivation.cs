using UnityEngine;

public class BossActivation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ManagerSubida.Instance.BossSpawn();
    }
}
