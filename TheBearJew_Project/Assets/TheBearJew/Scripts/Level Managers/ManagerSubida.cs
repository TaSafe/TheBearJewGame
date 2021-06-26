using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ManagerSubida : MonoBehaviour
{
    public static ManagerSubida Instance { get; private set; }

    [Header("Cutscenes")]
    [SerializeField] private VideoClip _videoBeforeBoss;
    [SerializeField] private VideoClip _videoAfterBoss;
    
    [Header("others")]
    [SerializeField] private Collider _bossActivation;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _bossUI;
    [SerializeField] private Transform _playerBossResposition;
    [SerializeField] private GameObject _blocksGroup;

    private bool[] handles = new bool[2];
    private bool allhandlesOn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void HandleOn()
    {
        for (int i = 0; i < handles.Length; i++)
        {
            if (!handles[i])
            {
                handles[i] = true;

                if (i == handles.Length - 1)
                    allhandlesOn = true;

                break;
            }
        }
    }

    public void BossSpawn()
    {
        if (!allhandlesOn) return;

        _bossActivation.enabled = false;
        
        VideoController.Instance.ChangeVideo(_videoBeforeBoss);
        VideoController.Instance.VideoActivate();
        VideoController.Instance.OnVideoEnd?.AddListener(BossActivation);

        _blocksGroup.SetActive(true);

        Player.Instance.PlayerBehaviour.SetPlayerPosition(_playerBossResposition.position);
        Player.Instance.PlayerBehaviour.RespawnPosition = _playerBossResposition.position;
    }

    private void BossActivation()
    {
        _bossUI.SetActive(true);
        _boss.SetActive(true);

        VideoController.Instance.OnVideoEnd?.RemoveListener(BossActivation);
    }

    public void BossDefeat()
    {
        VideoController.Instance.ChangeVideo(_videoAfterBoss);
        VideoController.Instance.VideoActivate();
        VideoController.Instance.OnVideoEnd?.AddListener(GameStatus.Instance.EndGame);
    }

    public void PlayerDeathBoss() => StartCoroutine(BossWait(1.4f));

    private IEnumerator BossWait(float time)
    {
        var bossScript = _boss.GetComponent<BossBehaviour>();

        if (bossScript.bossStatus.LifeSystem.CurrentLife + 40f > bossScript.bossStatus.enemyData.Life)
        {
            bossScript.bossStatus.LifeSystem.AddLife(bossScript.bossStatus.enemyData.Life - bossScript.bossStatus.LifeSystem.CurrentLife);
            BossUI.Instance.LifeBarSetValue((float)(BossUI.Instance.LifeBarGetCurrentValue() + (bossScript.bossStatus.enemyData.Life - bossScript.bossStatus.LifeSystem.CurrentLife)));
        }
        else if (bossScript.bossStatus.LifeSystem.CurrentLife + 40f > bossScript.bossStatus.enemyData.Life * .5f)
        {
            bossScript.bossStatus.LifeSystem.AddLife((bossScript.bossStatus.enemyData.Life / 2) - bossScript.bossStatus.LifeSystem.CurrentLife);
            BossUI.Instance.LifeBarSetValue(bossScript.bossStatus.enemyData.Life / 2);
        }
        else
        {
            bossScript.bossStatus.LifeSystem.AddLife(bossScript.bossStatus.LifeSystem.CurrentLife + 40f);
            BossUI.Instance.LifeBarSetValue((float)(BossUI.Instance.LifeBarGetCurrentValue() + 40f));
        }

        var bullets = GameObject.FindGameObjectsWithTag("Bullet");

        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }

        bossScript.enabled = false;
        yield return new WaitForSeconds(time);
        _boss.GetComponent<BossBehaviour>().enabled = true;
    }

}