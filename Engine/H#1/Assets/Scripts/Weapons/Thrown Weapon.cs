using UnityEngine;

public class ThrownWeapon : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 5f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float damageDelay;
    private ObjectPool pool;

    private GameObject owner;
    private bool canDealDamage = false;
    public void Init(ObjectPool poolRef)
    {
        
        pool = poolRef;
        CancelInvoke();
        Invoke(nameof(EnableDamage), damageDelay);
        Invoke(nameof(Deactive), lifeTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!canDealDamage || !GameManager.Instance.isPlay) return;
        IDamageable target = collision.collider.GetComponentInParent<IDamageable>();
        if (target != null)
        {
            float multiplier = 1f;
            DamageZone zone = collision.collider.GetComponent<DamageZone>();
            if (zone != null)
            {
                Debug.Log("Damage Zone");
                multiplier = zone.GetMultiplier();
            }
            int finalDamage = Mathf.RoundToInt(damage * multiplier);
            target.TakeDamage(finalDamage);
        }
        Deactive();
    }

    private void EnableDamage()
    {
        canDealDamage = true;
    }
    private void Deactive()
    {
        if(pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
