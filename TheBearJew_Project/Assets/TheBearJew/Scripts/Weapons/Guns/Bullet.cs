using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float destroyTime = 7f;

    private float damage;

    private void Awake() => Destroy(gameObject, destroyTime);

    private void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<IDamage>()?.Damage(damage);
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Bullet"))
            Destroy(gameObject);
    }

    public void SetDamage(float damage) => this.damage = damage;
}
