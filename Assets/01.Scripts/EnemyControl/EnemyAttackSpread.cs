using System.Collections;
using UnityEngine;

public class EnemyAttackSpread : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float attackDelay = 3f;
    [SerializeField] int bulletCount = 3;
    [SerializeField] float spreadAngle = 40f;

    Transform player;
    Coroutine attackCoroutine;

    void OnEnable()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;
        attackCoroutine = StartCoroutine(AttackRoutine());
    }

    void OnDisable()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }

    IEnumerator AttackRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        while (true)
        {
            yield return wait;
            if (player == null)
                continue;

            Vector2 baseDir = (player.position - transform.position).normalized;
            float startAngle = -spreadAngle * 0.5f;
            float interval = bulletCount > 1 ? spreadAngle / (bulletCount - 1) : 0;

            for (int i = 0; i < bulletCount; i++)
            {
                float a = startAngle + interval * i;
                Vector2 dir = Quaternion.Euler(0, 0, a) * baseDir;
                GameObject bulletObj = ObjectPoolManager.instance.Get(bullet, transform.position, Quaternion.identity);
                EnemyBullet eb = bulletObj.GetComponent<EnemyBullet>();
                eb.SetDir(dir);
                EnemySpawner.instance.allEnemyBullet.Add(eb);
            }

        }

    }




}