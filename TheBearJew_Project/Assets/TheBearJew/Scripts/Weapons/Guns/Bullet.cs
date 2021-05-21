using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BULLET_SPEED = 10f;

    private void Awake() => Destroy(gameObject, 7f);

    private void Update() => transform.Translate(Vector3.forward * BULLET_SPEED * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<IDamage>()?.Damage(20f);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
