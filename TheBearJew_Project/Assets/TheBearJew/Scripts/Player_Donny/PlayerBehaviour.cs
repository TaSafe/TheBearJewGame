using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamage
{
    [SerializeField] private float _totalLife = 100;

    private LifeSystem _lifeSystem;

    void Start()
    {
        _lifeSystem = new LifeSystem(_totalLife);
    }

    public void Damage(float damage)
    {
        if (!_lifeSystem.DeathCheck())
        {
            _lifeSystem.RemoveLife(damage);
            if (_lifeSystem.DeathCheck())
                Debug.Log($"Morri: {gameObject.name}. Vida = {_lifeSystem.CurrentLife}");
        }
    }

    //#### PARA TESTAR A VIDA ####
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Damage(20);
            Debug.Log(_lifeSystem.CurrentLife);
        }
    }
}
