
using System.Collections;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private bool _attacking;
    protected override void SpotAction()
    {
        animator.SetBool("isIdle", true);
        if (!_isWaiting) StartCoroutine(WaitAndChase(alertTime));
    }
    protected override void HitAction()
    {
        if (!_isWaiting) StartCoroutine(WaitAndChase(staggerTime));
    }

    protected override void AttackAction()
    {
        if (!_isWaiting)
        {
            StopAllCoroutines();
            animator.SetBool("isAttacking", true);
            StartCoroutine(FullAttack());
        }
        if (_attacking)
        {
            Attack();
        }
    }

    protected override void ChaseAction()
    {
        if (transform.position.x + 1 < _player.transform.position.x)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += moveSpeed * Time.deltaTime * Vector3.right;
        }
        else if (transform.position.x - 1 > _player.transform.position.x)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(1, 1, 1);
            transform.position += moveSpeed * Time.deltaTime * Vector3.left;
        }
        else {
            animator.SetBool("isWalking", false);
            ChangeState(EnemyState.ATTACKING);
        }
    }

    protected override void DieAction()
    {
        Destroy(gameObject);
    }

    private IEnumerator WaitAndChase(float waitTime)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        _isWaiting = false;
        ChangeState(EnemyState.CHASING);
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttacking", false);
    }

    private IEnumerator FullAttack()
    {
        _attacking = false;
        _isWaiting = true;
        yield return new WaitForSeconds(11f / 12f);
        _attacking = true;
        yield return new WaitForSeconds(5f / 12f);
        _attacking = false;
        yield return new WaitForSeconds(3f / 12f);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdle", true);
        yield return new WaitForSeconds(attackTime);
        _isWaiting = false;
        animator.SetBool("isIdle", false);
        ChangeState(EnemyState.CHASING);
    }
    private void Attack()
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(transform.localScale.z, transform.localScale.z), 0, Vector2.up, 0.1f, 128))
        {
            _player.GetComponent<PlayerActions>().TakeDamage(damage);
        }
    }

    public MeleeEnemy(float MaxHitPoints,
        float SightDistance,
        float IdleTime,
        float IdleVariation,
        Vector2 WanderLeftPoint,
        float WanderDistanceRight,
        float MoveSpeed,
        float AlertTime,
        float StaggerTime,
        float AttackTime)
    {
        maxHitPoints = MaxHitPoints;
        sightDistance = SightDistance;
        idleTime = IdleTime;
        idleVariation = IdleVariation;
        wanderLeftPoint = WanderLeftPoint;
        wanderDistanceRight = WanderDistanceRight;
        moveSpeed = MoveSpeed;
        alertTime = AlertTime;
        staggerTime = StaggerTime;
        attackTime = AttackTime;
    }
}


