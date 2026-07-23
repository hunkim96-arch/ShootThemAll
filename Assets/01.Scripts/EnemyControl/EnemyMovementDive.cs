using UnityEngine;

public class EnemyMovementDive : MonoBehaviour
{

    [SerializeField] float startSpeed = 1.5f;
    [SerializeField] float diveSpeed = 5f;
    [SerializeField] float diveDistance = 3f;

    Vector3 diveDir;
    float startX;
    bool isDiving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        startX = transform.position.x;
        isDiving = false;

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            diveDir = (playerObj.transform.position - transform.position).normalized;
        }
        else
            diveDir = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDiving)
        {
            transform.position += Vector3.left * startSpeed * Time.deltaTime;
            if (startX - transform.position.x >= diveDistance)
            {
                isDiving = true;
            }
        }
        else
        {
            transform.position += diveDir * diveSpeed * Time.deltaTime;
        }
    }
}
