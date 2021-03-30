using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer;

    private void Start()
    {
        timer = Time.deltaTime + 10f;
    }

    private void Update()
    {
        if (timer <= 0)
            Destroy(gameObject);
        else
            timer -= Time.deltaTime;

        transform.Translate(Vector3.forward * Time.deltaTime * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<IDamage>()?.Damage(10);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

}
