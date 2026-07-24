using UnityEngine;
using System.Collections;

public class BossLaser : MonoBehaviour
{
    [SerializeField] float warningTime = 1f;
    [SerializeField] float laserLength = 20f;
    [SerializeField] float laserWidth = 0.3f;
    [SerializeField] int damage = 1;
    LineRenderer lr;
    BoxCollider2D hitBox;

    void Awake()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.positionCount = 2;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = new Color(1f, 0f, 0f, 0.4f);
        lr.endColor = new Color(1f, 0f, 0f, 0.4f);

        hitBox = gameObject.AddComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
        hitBox.enabled = false;
    }

    public void Fire(Vector2 direction)
    {
        StartCoroutine(FireRoutine(direction));
    }

    IEnumerator FireRoutine(Vector2 direction)
    {
        Vector3 endPoint = transform.position + (Vector3)direction * laserLength;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, endPoint);

        
        yield return new WaitForSeconds(warningTime);

        
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
        lr.startColor = new Color(1f, 0.2f, 0.2f, 0.9f);
        lr.endColor = new Color(1f, 0.2f, 0.2f, 0.9f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        hitBox.size = new Vector2(laserLength, laserWidth);
        hitBox.offset = new Vector2(laserLength * 0.5f, 0f);
        hitBox.enabled = true;

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}