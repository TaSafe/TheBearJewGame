using System.Collections;
using UnityEngine;

public class BossBehaviour : MonoBehaviour, IDamage
{
    private enum State { ATTACK, RELOAD, PHASE_CHANGE }
    private State _currentState;

    [HideInInspector] public BossStatus bossStatus;
    private float fireRateTimer;
    private float shootTimer;
    private bool isCoolingDown;
    private bool hasChangedState;

    private void Start()
    {
        bossStatus = GetComponent<BossStatus>();
        ChangeState(State.ATTACK);
        shootTimer = Time.deltaTime + bossStatus.shootTime;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.ATTACK:
                AttackFromFar();
                break;
            case State.RELOAD:
                Reload();
                break;
            case State.PHASE_CHANGE:
                PhaseChange();
                break;
        }
    }

    private void ChangeState(State state) => _currentState = state;

    public void Damage(float damage, bool bat = false)
    {
        if (_currentState == State.PHASE_CHANGE) return;

        bossStatus.LifeSystem.RemoveLife(damage);
        BossUI.Instance.LifeBarSetValue((float)(BossUI.Instance.LifeBarGetCurrentValue() - damage));

        if (bossStatus.LifeSystem.IsDead())
        {
            GetComponent<BossBehaviour>().enabled = false;
            ManagerSubida.Instance.BossDefeat();
        }

        if (bossStatus.LifeSystem.CurrentLife < bossStatus.enemyData.Life * 0.5 && !hasChangedState)
        {
            hasChangedState = true;
            ChangeState(State.PHASE_CHANGE);
        }
    }

    private void AttackFromFar()
    {
        if (shootTimer < 0f && !isCoolingDown)
            ChangeState(State.RELOAD);
        else if (!isCoolingDown)
        {
            transform.LookAt(PlayerInput.Instance.transform.position, Vector3.up);

            if (fireRateTimer <= 0f)
            {
                GameObject bullet = Instantiate(bossStatus.Bullet, bossStatus.Muzzle.position, transform.rotation);
                bullet.GetComponent<Bullet>().SetBullet(bossStatus.smgDamage, gameObject.name);
                FMODUnity.RuntimeManager.PlayOneShot(bossStatus.smgShootSound);

                fireRateTimer += Time.deltaTime + bossStatus.smgFireRate;
            }
            else
                fireRateTimer -= Time.deltaTime;

            shootTimer -= Time.deltaTime;
        }
    }
    
    private void Reload()
    {
        if (!isCoolingDown)
            StartCoroutine(ShootCooldown(bossStatus.shootCooldown));

        if (!bossStatus.navMeshAgent.hasPath)
            bossStatus.navMeshAgent.SetDestination(bossStatus.reloadPoints[Random.Range(0, bossStatus.reloadPoints.Count)].position);

        if (!bossStatus.animator.GetBool("isMoving") && bossStatus.navMeshAgent.velocity != Vector3.zero)
            bossStatus.animator.SetBool("isMoving", true);
    }

    private IEnumerator ShootCooldown(float cooldown)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldown);
        bossStatus.navMeshAgent.ResetPath();
        bossStatus.animator.SetBool("isMoving", false);
        shootTimer = Time.deltaTime + bossStatus.shootTime;
        isCoolingDown = false;
        ChangeState(State.ATTACK);
    }

    private void PhaseChange()
    {
        StopAllCoroutines();
        bossStatus.navMeshAgent.ResetPath();
        bossStatus.animator.SetBool("isMoving", false);
        isCoolingDown = false;

        BossUI.Instance.LifeBarSetMaxValue(bossStatus.enemyData.Life / 2);
        BossUI.Instance.LifeBarSetBackgroundColor(bossStatus.defaultBarColor);
        BossUI.Instance.LifeBarSetColor(bossStatus.secondBarColor);

        bossStatus.navMeshAgent.speed *= 1.5f;
        bossStatus.navMeshAgent.angularSpeed += 100f;
        bossStatus.smgFireRate -= 0.07f;

        ChangeState(State.ATTACK);
    }

}

//Possível Shotgun
//for (int i = 0; i < 8; i++)
//{
//    GameObject bullet = Instantiate(
//        bossStatus.Bullet, 
//        bossStatus.Muzzle.position, 
//        Quaternion.Euler(new Vector3(Random.Range(-20f, 21f), transform.rotation.eulerAngles.y + Random.Range(-20f, 21f), 0f)));
//    bullet.GetComponent<Bullet>().SetDamage(15f);   //TRANSFORMAR EM VARIÁVEL
//}