using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    private PlayerBehaviour _playerBehaviour;

    private void Start()
    {
        var playerTemp = GameObject.FindGameObjectWithTag("Player").gameObject;
        _playerBehaviour = playerTemp.GetComponent<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerBehaviour.RespawnPosition = transform.position;
        }
    }

}
