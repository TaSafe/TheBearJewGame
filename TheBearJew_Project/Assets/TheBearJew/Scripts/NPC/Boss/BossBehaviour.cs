using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour, IDamage
{
    private enum State { ATTACK, RELOAD }
    private State _currentState;

    private BossStatus bossStatus;
    private float fireRateTimer;
    private float shootTimer;
    private bool isCoolingDown;

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
        }
    }

    private void ChangeState(State state) => _currentState = state;

    public void Damage(float damage, bool bat = false)
    {
        bossStatus.LifeSystem.RemoveLife(damage);
        BossUI.Instance.LifeBarSetValue(bossStatus.LifeSystem.CurrentLife);

        if (bossStatus.LifeSystem.IsDead())
        {
            //Faz algo
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
                bullet.GetComponent<Bullet>().SetDamage(bossStatus.smgDamage);
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

        if (!bossStatus.animator.GetBool("isMoving"))
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

    //private void AttackFromClose()
    //{
    //    transform.LookAt(PlayerInput.Instance.transform.position, Vector3.up);

    //    if (timer <= 0f)
    //    {
            

    //        if (MathUtilities.ConeChecker(gameObject, PlayerInput.Instance.transform.position, 45f))
    //        {
    //            PlayerInput.Instance.PlayerBehaviour.Damage(bossStatus.flamethrowerDamage);
    //            Debug.DrawLine(transform.position, PlayerInput.Instance.transform.position);
    //        }

    //        timer += Time.deltaTime + bossStatus.flamethrowerFireRate;
    //    }
    //    else
    //        timer -= Time.deltaTime;

    //    if (Vector3.Distance(transform.position, PlayerInput.Instance.transform.position) > bossStatus.RangeFarAttack)
    //    {
    //        timer = 0f;
    //        ChangeState(State.FAR_ATTACK);
    //    }
    //}

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