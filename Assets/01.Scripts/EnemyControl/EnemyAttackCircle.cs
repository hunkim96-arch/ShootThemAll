using System.Collections;
using UnityEngine;
public class EnemyAttackCircle : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float attackDelay = 3f;
    [SerializeField] int bulletCount = 12;

    Coroutine attackCoroutine;

    void OnEnable()
    {
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
            float interval = 360f / bulletCount;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = interval * i;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
                GameObject bulletObj = ObjectPoolManager.instance.Get(bullet, transform.position, Quaternion.identity);
                EnemyBullet eb = bulletObj.GetComponent<EnemyBullet>();
                eb.SetDir(dir);
                EnemySpawner.instance.allEnemyBullet.Add(eb);
            }

        }


    }



}