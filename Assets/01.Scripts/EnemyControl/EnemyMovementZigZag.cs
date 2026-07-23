using UnityEngine;

public class EnemyMovementZigZag : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float amplitude = 2f;
    [SerializeField] float frequnecy = 3f;

    float startY;
    float elapsed;

    void OnEnable()
    {
        startY = transform.position.y;
        elapsed = 0f;
    }


    void Update()
    {
        elapsed += Time.deltaTime;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        float y = startY + Mathf.Sin(Time.time * frequnecy) * amplitude;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        float tangent = Mathf.Cos(elapsed * frequnecy) * amplitude * frequnecy;
        float angle = Mathf.Atan2(tangent, -moveSpeed) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

    }
}
