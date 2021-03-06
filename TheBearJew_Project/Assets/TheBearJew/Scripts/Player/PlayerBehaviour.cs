using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour, IDamage
{
    [SerializeField] private float _totalLife = 100;

    [Header("Visual")]
    [SerializeField] private Material _donnyWhithoutMilitaryUniform;
    [SerializeField] private GameObject _backpack;
    [SerializeField] private GameObject _helmet;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _hair;

    private LifeSystem _lifeSystem;
    public Vector3 RespawnPosition { get; set; }

    void Start()
    {
        _lifeSystem = new LifeSystem(_totalLife);
        UiHUD.Instance.SetLifeBar(_totalLife);
    }

    public void Damage(float damage, bool bat = false)
    {
        _lifeSystem.RemoveLife(damage);

        UiHUD.Instance.ChangeLifeBar(_lifeSystem.CurrentLife);

        //som de dano
        FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/damage");

        if (_lifeSystem.IsDead())
        {
            //som de morte
            FMODUnity.RuntimeManager.PlayOneShot("event:/Donny/death");
            Respawn();

            if (SceneManager.GetActiveScene().name == "Subida_Para_a_Abadia")
                ManagerSubida.Instance.PlayerDeathBoss();
        }
    }

    public void Respawn()
    {
        SetPlayerPosition(RespawnPosition);

        _lifeSystem.AddLife(_totalLife);
        UiHUD.Instance.SetLifeBar(_totalLife);
    }

    public void SetPlayerPosition(Vector3 position)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = position;
        GetComponent<CharacterController>().enabled = true;
    }

    public void VisualChange()
    {
        _body.GetComponent<SkinnedMeshRenderer>().material = _donnyWhithoutMilitaryUniform;
        _backpack.SetActive(false);
        _helmet.SetActive(false);
        _hair.SetActive(true);
    }
}
