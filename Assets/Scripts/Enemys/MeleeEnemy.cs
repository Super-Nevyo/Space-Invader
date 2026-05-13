
using System.Collections;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    
    protected override void SpotAction()
    {
        if (!_isWaiting) StartCoroutine(WaitAndChase(alertTime));
    }
    protected override void HitAction()
    {
        if (!_isWaiting) StartCoroutine(WaitAndChase(staggerTime));
    }

    protected override void AttackAction()
    {
        if (!_isWaiting) StartCoroutine(WaitAndChase(attackTime));
    }

    protected override void ChaseAction()
    {
        if (transform.position.x + 1 < _player.transform.position.x)
        { 
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.position += moveSpeed * Time.deltaTime * Vector3.right;
        }
        else if (transform.position.x - 1 > _player.transform.position.x)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            transform.position += moveSpeed * Time.deltaTime * Vector3.left;
        }
        else ChangeState(EnemyState.ATTACKING);
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


