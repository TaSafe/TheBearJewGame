using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    private PlayerBehaviour _playerBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_playerBehaviour) _playerBehaviour = other.GetComponentInParent<PlayerBehaviour>();
            _playerBehaviour.RespawnPosition = transform.position;
        }
    }

}
