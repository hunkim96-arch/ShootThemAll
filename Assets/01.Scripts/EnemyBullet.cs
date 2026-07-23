using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 15f;

    Rigidbody2D rb;

    int damage; 
    float lifeTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        damage = 1;
        lifeTime = 6f;
        Destroy(gameObject, lifeTime);
    }

    public void SetDir(Vector2 dir)
    {
        rb.linearVelocity= dir.normalized * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer== LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        if (EnemySpawner.instance != null)
            EnemySpawner.instance.allEnemyBullet.Remove(this);
    }


}
