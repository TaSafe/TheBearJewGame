using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamage
{
    [SerializeField] private float _totalLife = 100;

    private LifeSystem _lifeSystem;
    public Vector3 RespawnPosition { get; set; }

    void Start()
    {
        _lifeSystem = new LifeSystem(_totalLife);
        UiHUD.instance.SetLifeBar(_totalLife);
    }

    public void Damage(float damage, bool bat = false)
    {
        _lifeSystem.RemoveLife(damage);

        UiHUD.instance.ChangeLifeBar(_lifeSystem.CurrentLife);

        if (_lifeSystem.DeathCheck())
        {
            //som de morte
            FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/death");
            Respawn();
        }
    }

    public void Respawn()
    {
        SetPlayerPosition(RespawnPosition);

        _lifeSystem.AddLife(_totalLife);
        UiHUD.instance.SetLifeBar(_totalLife);
    }

    public void SetPlayerPosition(Vector3 position)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = position;
        GetComponent<CharacterController>().enabled = true;
    }
}
