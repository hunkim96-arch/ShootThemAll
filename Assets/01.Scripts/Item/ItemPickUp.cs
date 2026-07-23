using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    IItemEffect effect;

    void Awake()
    {
        effect = GetComponent<IItemEffect>();
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (effect != null)
            {
                effect.OnPickUp(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
