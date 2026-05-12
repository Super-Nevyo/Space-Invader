using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseEnemy : MonoBehaviour
{

    protected int hitPoints;
    [SerializeField] protected int maxHitPoints;
    [SerializeField] protected float sightDistance;
    [SerializeField] protected float idleTime;
    [SerializeField] protected float idleVariation;
    [SerializeField] protected Vector2 wanderLeftPoint;
    [SerializeField] protected float wanderDistanceRight;
    [SerializeField] protected float moveSpeed;
    protected bool _isWaiting;

    private Vector2 _wanderTarget;
    private EnemyState _currentState;
    private GameObject _player;

    protected virtual void Awake()
    {
        _currentState = EnemyState.IDLE;
    }

    protected void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
        if (!_isWaiting) StartCoroutine(Idle(idleTime, idleVariation));
        if (IsPlayerSpottable())
        {
            ChangeState(EnemyState.SPOTTED);
        }
    }
    protected virtual void WanderAction()
    {
        if (_wanderTarget == Vector2.zero)
        {
            _wanderTarget = new Vector2(Random.Range(wanderLeftPoint.x, wanderLeftPoint.x + wanderDistanceRight), wanderLeftPoint.y);
        }
        else if (Mathf.Abs(transform.position.x - _wanderTarget.x) < 0.2)
        {
            if (transform.position.x < _wanderTarget.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += moveSpeed * Time.deltaTime * Vector3.right;
            }
            if (transform.position.x > _wanderTarget.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += moveSpeed * Time.deltaTime * Vector3.left;
            }
        }
        else
        {
            ChangeState(EnemyState.IDLE);
            _wanderTarget = Vector2.zero;
        }
        if (IsPlayerSpottable())
        {
            ChangeState(EnemyState.SPOTTED);
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

    protected IEnumerator Idle(float waitTime, float waitVariation)
    {
        _isWaiting = true;
        yield return new WaitForSeconds(waitTime + Random.Range(0, waitVariation));
        ChangeState(EnemyState.WANDER);
        _isWaiting = false;
    }

    protected virtual bool IsPlayerSpottable()
    {
        if (_player.transform.position.x - transform.position.x > sightDistance) return false;
        return true;
    }

}
