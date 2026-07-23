using UnityEngine;

public class EnemyMovementStraight : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float moveDistance = 5f;

    Vector3 targetPos;


    private void OnEnable()
    {
        targetPos = transform.position + Vector3.left * moveDistance;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
