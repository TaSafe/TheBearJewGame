using UnityEngine;

public class EnemySelfStatus : MonoBehaviour, IDamage
{
    [SerializeField] private EnemyData _enemyData;

    private LifeSystem _lifeSystem;

    void Start()
    {
        _lifeSystem = new LifeSystem(_enemyData.Life);
    }

    public void Damage(float damage)
    {
        if (!_lifeSystem.DeathCheck())
        {
            _lifeSystem.RemoveLife(damage);
            if (_lifeSystem.DeathCheck())
                Destroy(gameObject);
                    //Debug.Log($"Morri: {gameObject.name}. Vida = {_lifeSystem.CurrentLife}");
        }
    }

    //#### PARA TESTAR A VIDA ####
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Damage(20);
            Debug.Log($"{_enemyData.Name} possui {_lifeSystem.CurrentLife} de vida restante.");
        }
    }

}
