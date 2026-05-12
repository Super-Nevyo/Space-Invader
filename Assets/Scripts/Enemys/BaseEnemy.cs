using System.Collections;
using UnityEngine;
public abstract class BaseEnemy : MonoBehaviour
{

    protected int hitPoints;
    [SerializeField] protected int maxHitPoints;
    [SerializeField] protected float sightDistance;
    [SerializeField] protected float idleTime;
    [SerializeField] protected float idleVariation;
    [SerializeField] protected Vector2 wanderLeftPoint;
    [SerializeField] protected float wanderDistanceRight;
    protected bool _isWaiting;

    private Vector2 _wanderTarget;
    private EnemyState _currentState;

    protected virtual void Awake()
    {
        _currentState = EnemyState.IDLE;
    }
    private void FixedUpdate()
    {
        if (_currentState == EnemyState.IDLE)
        {
            IdleAction();
        }
        else if (_currentState == EnemyState.WANDER)
        {
            WanderAction();
        }
        else if (_currentState == EnemyState.SPOTTED)
        {
            SpotAction();
        }
        else if (_currentState == EnemyState.HIT)
        {
            HitAction();
        }
        else if (_currentState == EnemyState.ATTACKING)
        {
            AttackAction();
        }
        else if (_currentState == EnemyState.CHASING)
        {
            ChaseAction();
        }
        else if (_currentState == EnemyState.DYING)
        {
            DieAction();
        }
    }
    protected virtual void IdleAction()
    {
        if (!_isWaiting) StartCoroutine(idle(idleTime, idleVariation));
    }
    protected virtual void WanderAction()
    {
        if (_wanderTarget == null)
        {
            _wanderTarget = new Vector2(Random.Range(wanderLeftPoint.x, wanderLeftPoint.x + wanderDistanceRight), wanderLeftPoint.y);
        }
        else
        {

        }
    }
    protected abstract void SpotAction();
    protected abstract void HitAction();
    protected abstract void AttackAction();
    protected abstract void ChaseAction();
    protected abstract void DieAction();

    protected virtual void ChangeState(EnemyState NewState)
    {
        _currentState = NewState;
    }

    protected IEnumerator idle(float waitTime, float waitVariation)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime + Random.Range(0, waitVariation));
        ChangeState(EnemyState.WANDER);
        _isWaiting = false;
    }
   

}
