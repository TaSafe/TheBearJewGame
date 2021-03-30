using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamage
{
    [SerializeField] private float _totalLife = 100;

    private LifeSystem _lifeSystem;
    public Vector3 RespawnPosition { get; set; }

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
                Respawn();
                //Debug.Log($"Morri: {gameObject.name}. Vida = {_lifeSystem.CurrentLife}");
        }
    }

    public void Respawn()
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = RespawnPosition;
        GetComponent<CharacterController>().enabled = true;

        _lifeSystem.AddLife(_totalLife);
        UiInteraction.instance.SetLifeBar(_totalLife);
    }

    //#### PARA TESTAR A VIDA ####
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //        Damage(20);
    //}
}
