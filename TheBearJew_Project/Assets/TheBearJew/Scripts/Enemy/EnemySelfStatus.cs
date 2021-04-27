using UnityEngine;

public class EnemySelfStatus : MonoBehaviour, IDamage
{
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private GameObject _lifeUi;
    [SerializeField] private GameObject[] _gunsToDrop;

    private LifeSystem _lifeSystem;
    private UiLifeEnemy _uiLife;

    void Start()
    {
        _lifeSystem = new LifeSystem(_enemyData.Life);

        var uiLife = Instantiate(_lifeUi, _lifeUi.transform.position, Quaternion.Euler(new Vector3(45f, 0f, 0f)));
        _uiLife = uiLife.GetComponent<UiLifeEnemy>();
        _uiLife.SetValues(transform, _enemyData.Life);
    }

    public void Damage(float damage, bool bat = false)
    {
        _lifeSystem.RemoveLife(damage);
        _uiLife.ChangeValue(_lifeSystem.CurrentLife);

        DeathCheck(bat);
    }

    //A execu��o t� bem torta, tem que pensar melhor depois - 27/04/2021
    private void DeathCheck(bool batAttack)
    {
        if (_lifeSystem.DeathCheck() || /*execu��o*/ batAttack && _lifeSystem.CurrentLife < 40f)
        {
            _uiLife.Destroy();

            //Drop da arma
            var rand = Random.Range(0, _gunsToDrop.Length);
            Instantiate(_gunsToDrop[rand], transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (_lifeSystem.CurrentLife < 40f) //abilita execu��o
            _uiLife.ShowExecutionFeedback();
    }
}
