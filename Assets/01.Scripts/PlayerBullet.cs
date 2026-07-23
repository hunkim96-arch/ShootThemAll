using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float moveSpeed= 8f;
    float lifeTime;

    Rigidbody2D rb;

    public int damage;

    Coroutine returnCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        lifeTime = 3f;
        damage = 1;
        rb.linearVelocity = Vector3.right * moveSpeed;
        returnCoroutine = StartCoroutine(ReturnAfterTime());        
    }



    void OnDisable()
    {
        if (returnCoroutine != null)
        {
            StopCoroutine(returnCoroutine);
        }
    }

    System.Collections.IEnumerator ReturnAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPoolManager.instance.Return(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IDamageable target = collision.gameObject.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            ObjectPoolManager.instance.Return(gameObject);
        }
    }
}
