using UnityEngine;

public class EnemyMovementHoming : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float turnSpeed = 90f;
    Vector2 currentDir;


    void OnEnable()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            currentDir = ((Vector2)playerObj.transform.position - (Vector2)transform.position).normalized;
        }
        else
        {
            currentDir = Vector2.left;
        }
    }


    void Update()
    {
        transform.position += (Vector3)currentDir * moveSpeed * Time.deltaTime;
        float angle = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
