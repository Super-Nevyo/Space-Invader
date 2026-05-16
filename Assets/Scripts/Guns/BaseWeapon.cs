using System.Collections;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected float weaponRange;
    [SerializeField] protected Vector2 weaponStart;
    [SerializeField] protected Vector2 shootDirection;
    [SerializeField] protected float shootCooldown;
    [SerializeField] protected float damage;

    [SerializeField] private PlayerActions playerActions;
    protected RaycastHit2D[] hits = new RaycastHit2D[1];
    protected bool onCooldown = false;
    private IHittable _hittable;

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
            ShootAction();
            if (hits[0].collider != null && hits.Length > 0)
            foreach(RaycastHit2D hit in hits)
            {
                 _hittable = hit.transform.GetComponent<IHittable>();
                if (_hittable != null)
                {
                    _hittable.OnHit(damage);
                }
            }
        }
    }

    private IEnumerator ShootCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(shootCooldown);
        onCooldown = false;
    }

    protected virtual void ShootAction()
    {
        if (playerActions.transform.localScale.x > 0)
        {
            hits = new RaycastHit2D[1] { Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + weaponStart, new Vector2(transform.position.x, transform.position.y) + shootDirection, weaponRange) };
            Debug.DrawLine(new Vector2(transform.position.x, transform.position.y) + weaponStart, new Vector2(transform.position.x, transform.position.y) + weaponRange * shootDirection.normalized, Color.white, 0.1f);
        }
        if (playerActions.transform.localScale.x < 0)
        {
            hits = new RaycastHit2D[1] { Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + weaponStart * new Vector2(-1, 1), new Vector2(transform.position.x, transform.position.y) + shootDirection * new Vector2(-1, 1), weaponRange) };
            Debug.Log(shootDirection * Vector2.left);
            Debug.DrawLine(new Vector2(transform.position.x, transform.position.y) + weaponStart * new Vector2(-1,1), new Vector2(transform.position.x, transform.position.y) + weaponRange * shootDirection.normalized * new Vector2(-1, 1), Color.white, 0.1f);
        }
    }    

}
