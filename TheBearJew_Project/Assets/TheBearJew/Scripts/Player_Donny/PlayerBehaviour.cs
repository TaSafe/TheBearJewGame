using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamage
{
    [SerializeField] private float _totalLife = 100;

    private LifeSystem _lifeSystem;

    void Start()
    {
        _lifeSystem = new LifeSystem(_totalLife);
        UiInteraction.instance.SetLifeBar(_totalLife);
    }

    public void Damage(float damage)
    {
        if (!_lifeSystem.DeathCheck())
        {
            _lifeSystem.RemoveLife(damage);

            UiInteraction.instance.ChangeLifeBar(_lifeSystem.CurrentLife);

            Debug.Log(_lifeSystem.CurrentLife);

            if (_lifeSystem.DeathCheck())
                Debug.Log($"Morri: {gameObject.name}. Vida = {_lifeSystem.CurrentLife}");
        }
    }

    //#### PARA TESTAR A VIDA ####
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            Damage(20);
    }
}
