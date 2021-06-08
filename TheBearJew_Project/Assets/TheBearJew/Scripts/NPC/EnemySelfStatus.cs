using UnityEngine;
using UnityEngine.Events;

public class EnemySelfStatus : MonoBehaviour, IDamage
{
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private GameObject _lifeUi;
    [SerializeField] private GameObject[] _gunsToDrop;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private float _gunDropeedHeight;

    private LifeSystem _lifeSystem;
    private UiLifeEnemy _uiLife;

    void Start()
    {
        _lifeSystem = new LifeSystem(_enemyData.Life);

        GameObject uiLife = Instantiate(_lifeUi, _lifeUi.transform.position, Quaternion.Euler(new Vector3(45f, 0f, 0f)));
        _uiLife = uiLife.GetComponent<UiLifeEnemy>();
        _uiLife.SetValues(transform, _enemyData.Life);
    }

    private void Update()
    {
        //Teste de dano
        if (Input.GetKeyDown(KeyCode.P))
            Damage(40f);
    }

    public void Damage(float damage, bool bat = false)
    {
        _lifeSystem.RemoveLife(damage);
        _uiLife.ChangeValue(_lifeSystem.CurrentLife);

        DeathCheck(bat);
    }

    //A 'execução' tá bem torta, tem que pensar melhor depois - 27/04/2021
    private void DeathCheck(bool batAttack)
    {
        if (_lifeSystem.IsDead() || batAttack && _lifeSystem.CurrentLife < 40f)
        {
            _uiLife.Destroy();

            //Drop da arma
            var random = Random.Range(0, _gunsToDrop.Length);
            Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y + _gunDropeedHeight, transform.position.z);
            Instantiate(_gunsToDrop[random], dropPosition, Quaternion.Euler(0f, 90f, 0f));

            OnDeath?.Invoke();

            Destroy(gameObject);
        }
        else if (_lifeSystem.CurrentLife < 40f) //abilita 'execução'
            _uiLife.ShowExecutionFeedback();
    }
}