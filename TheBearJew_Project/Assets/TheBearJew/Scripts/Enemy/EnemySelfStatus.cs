using UnityEngine;

public class EnemySelfStatus : MonoBehaviour, IDamage
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private GameObject _lifeUi;

    private LifeSystem _lifeSystem;
    private UiLifeEnemy _uiLife;

    void Start()
    {
        _lifeSystem = new LifeSystem(_enemyData.Life);

        var uiLife = Instantiate(_lifeUi);
        _uiLife = uiLife.GetComponent<UiLifeEnemy>();
        _uiLife.SetValues(transform, _enemyData.Life);
    }

    public void Damage(float damage)
    {
        if (!_lifeSystem.DeathCheck())
        {
            _lifeSystem.RemoveLife(damage);
            _uiLife.ChangeValue(_lifeSystem.CurrentLife);
            //Debug.Log($"{_enemyData.Name} possui {_lifeSystem.CurrentLife} de vida restante.");

            DeathCheck();
        }
    }

    private void DeathCheck()
    {
        if (_lifeSystem.DeathCheck())
        {
            _uiLife.Destroy();
            Destroy(gameObject);
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
