using System.Collections;
using UnityEngine;

public class EnemyAttackSingle : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] float attackDelay = 3f;

    Transform player;
    Coroutine attackCoroutine;


    void OnEnable()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        attackCoroutine = StartCoroutine(AttackRoutine());
    }

    void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }


    IEnumerator AttackRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        while (true)
        {
            yield return wait;
            if (player == null)
                continue;

            GameObject bulletObj = ObjectPoolManager.instance.Get(bullet, transform.position, Quaternion.identity);
            EnemyBullet eb = bulletObj.GetComponent<EnemyBullet>();
            Vector2 dir = player.position - transform.position;
            eb.SetDir(dir);
            EnemySpawner.instance.allEnemyBullet.Add(eb);
        }
    }


}
