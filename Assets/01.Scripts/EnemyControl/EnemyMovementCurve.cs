using System.Timers;
using UnityEngine;

public class EnemyMovementCurve : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float curveStrength = 1.5f;

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

        float y = startY + curveStrength * Mathf.Sin(elapsed * 0.6f) * (elapsed * 0.4f);
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

}
