using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    Coroutine currentPattern;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartPhase(int phase)
    {
        StopAllPatterns();
        switch (phase)
        {
            case 0:
                currentPattern = StartCoroutine(SectorPatternLoop());
                break;
            case 1:
                currentPattern = StartCoroutine(CirclePatternLoop());
                break;
            case 2:
                currentPattern = StartCoroutine(SpiralPatternLoop());
                break;
        }
    }

    public void StopAllPatterns()
    {
        if (currentPattern != null)
            StopCoroutine(currentPattern);
    }


    void FireBullet(Vector2 direction)
    {
        GameObject b = ObjectPoolManager.instance.Get(bullet, transform.position, Quaternion.identity);
        EnemyBullet eb = b.GetComponent<EnemyBullet>();
        eb.SetDir(direction);
        EnemySpawner.instance.allEnemyBullet.Add(eb);
        Debug.Log("발사 방향 : " + direction);
    }


    // 부채꼴
    IEnumerator SectorPatternLoop()
    {
        WaitForSeconds burstShootWait = new WaitForSeconds(0.08f);
        WaitForSeconds cooldownWait = new WaitForSeconds(1.3f);

        while (true)
        {
            int bulletCount = 10;
            float angle = 50f;
            float randomOffset = Random.Range(-20f, 20f);
            float subAngle = -angle * 0.5f + randomOffset;
            float interval = angle / (bulletCount - 1);
            for (int i = 0; i < bulletCount; i++)
            {
                float a = subAngle + interval * i;
                Vector2 direction = Quaternion.Euler(0, 0, a) * Vector2.left;
                FireBullet(direction);
                yield return burstShootWait;
            }
            yield return cooldownWait;
        }
    }

    // 원형 발사
    IEnumerator CirclePatternLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(0.8f);

        float rotationOffset = 0f;
        while (true)
        {
            int bulletCount = 20;
            float interval = 360f / bulletCount;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = interval * i + rotationOffset;
                Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.right;
                FireBullet(direction);
            }
            rotationOffset += 15f;
            yield return wait;
        }
    }


    // 나선 발사
    IEnumerator SpiralPatternLoop()
    {
        float angleOffset = 0f;
        float rotateDir = 1f;
        float elapsed = 0f;
        float rotateSpeed = 20f;
        int armCount = 3;
        WaitForSeconds wait = new WaitForSeconds(0.15f);

        while (true)
        {
            for (int i = 0; i < armCount; i++)
            {
                float armAngle = angleOffset + (360f / armCount) * i;
                Vector2 direction = Quaternion.Euler(0f, 0f, angleOffset) * Vector2.right;
                FireBullet(direction);
            }

            angleOffset += 20f * rotateDir;
            elapsed += 0.05f;

            if (elapsed >= 2.5f)
            {
                rotateDir *= -1f;
                rotateSpeed = Random.Range(15f, 30f);
                elapsed = 0f;
            }
            yield return wait;
        }
    }


}
