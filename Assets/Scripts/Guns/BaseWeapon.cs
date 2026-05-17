using System.Collections;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected float weaponRange;
    [SerializeField] protected Vector2 weaponStart;
    [SerializeField] protected Vector2 shootDirection;
    [SerializeField] protected float shootCooldown;
    [SerializeField] protected float damage;
    [SerializeField] protected GameObject tracer;

    [SerializeField] private PlayerActions playerActions;
    protected RaycastHit2D[] hits = new RaycastHit2D[1];
    protected bool onCooldown = false;
    private IHittable _hittable;
    private GameObject _tempTracer;

    private void OnEnable()
    {
        playerActions.ShootEvent += OnShoot;
    }
    private void OnDisable()
    {
        playerActions.ShootEvent -= OnShoot;
    }
    private void OnShoot()
    {
        if (!onCooldown)
        {
            StartCoroutine(ShootCooldown());
            if (playerActions.transform.localScale.x > 0)
                ShootAction(new Vector2(transform.position.x, transform.position.y) + weaponStart, shootDirection, weaponRange);
            else if (playerActions.transform.position.y < 0)
                ShootAction(new Vector2(transform.position.x, transform.position.y) + weaponStart * new Vector2(-1, 1), shootDirection * new Vector2(-1, 1), weaponRange);
            if (hits[0].collider != null && hits.Length > 0)
            {
                if (playerActions.transform.localScale.x > 0)
                    CreateTracer(new Vector2(transform.position.x, transform.position.y) + weaponStart, hits[hits.Length - 1].point);
                if (playerActions.transform.localScale.x < 0)
                    CreateTracer(new Vector2(transform.position.x, transform.position.y) + weaponStart * new Vector2(-1, 1), hits[hits.Length - 1].point);
                foreach (RaycastHit2D hit in hits)
                {
                    _hittable = hit.transform.GetComponent<IHittable>();
                    if (_hittable != null)
                    {
                        _hittable.OnHit(damage);
                    }
                }
            }
            else
            {
                if (playerActions.transform.localScale.x > 0)
                    CreateTracer(new Vector2(transform.position.x, transform.position.y) + weaponStart, new Vector2(transform.position.x, transform.position.y) + weaponRange * shootDirection);
                if (playerActions.transform.localScale.x < 0)
                        CreateTracer(new Vector2(transform.position.x, transform.position.y) + weaponStart * new Vector2(-1, 1), new Vector2(transform.position.x, transform.position.y) + weaponRange * shootDirection * new Vector2(-1, 1));

            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(shootCooldown);
        onCooldown = false;
    }

    protected virtual void ShootAction(Vector2 startPoint, Vector2 direction, float distance)
    {

        hits = new RaycastHit2D[1] { Physics2D.Raycast(startPoint, direction, distance) };

    }    
    protected virtual void CreateTracer(Vector2 startPoint, Vector2 hitPoint)
    {
        
        Vector3 dir = new Vector3 (hitPoint.x - startPoint.x, hitPoint.y - startPoint.y, 0 );
        Quaternion rot = Quaternion.Euler(0,0, Quaternion.LookRotation(dir, new Vector3 (0,0,-1)).eulerAngles.z);
        _tempTracer = Instantiate(tracer, 0.5f * dir + new Vector3 (startPoint.x, startPoint.y, 0), rot , null);
        _tempTracer.transform.localScale = new Vector3(1, dir.magnitude/4, 1);
        Destroy(_tempTracer,0.1f);
    }

}
