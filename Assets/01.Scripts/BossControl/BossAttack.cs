using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossPatternType
{
    Sector,
    Circle,
    Spiral,
    AimedBurst,
    Cross,
    RandomSpray,
    Wave,
    Pincer,
    Laser
}

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform leftSpawnPoint;
    [SerializeField] Transform rightSpawnPoint;
    [SerializeField] GameObject laserWarningPrefab;

    [SerializeField] List<BossPatternType> phase1Patterns = new List<BossPatternType>();
    [SerializeField] List<BossPatternType> phase2Patterns = new List<BossPatternType>();
    [SerializeField] List<BossPatternType> phase3Patterns = new List<BossPatternType>();
    
    List<Coroutine> activeCoroutines = new List<Coroutine>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartPhase(int phase)
    {
        StopAllPatterns();

        List<BossPatternType> patterns = GetPatternsForPhase(phase);
        foreach (BossPatternType pattern in patterns)
        {
            Coroutine c = StartCoroutine(GetPatternRoutine(pattern));
            activeCoroutines.Add(c);
        }
    }


    List<BossPatternType> GetPatternsForPhase(int phase)
    {
        if (phase == 0) return phase1Patterns;
        if (phase == 1) return phase2Patterns;
        return phase3Patterns;
    }


    IEnumerator GetPatternRoutine(BossPatternType pattern)
    {
        switch (pattern)
        {
            case BossPatternType.Sector:
                yield return StartCoroutine(SectorPatternLoop());
                break;
            case BossPatternType.Circle:
                yield return StartCoroutine(CirclePatternLoop());
                break;
            case BossPatternType.Spiral:
                yield return StartCoroutine(SpiralPatternLoop());
                break;
            case BossPatternType.AimedBurst:
                yield return StartCoroutine(AimedBurstLoop());
                break;
            case BossPatternType.Cross:
                yield return StartCoroutine(CrossPatternLoop());
                break;
            case BossPatternType.RandomSpray:
                yield return StartCoroutine(RandomSprayLoop());
                break;
            case BossPatternType.Wave:
                yield return StartCoroutine(WaveLoop());
                break;
            case BossPatternType.Pincer:
                yield return StartCoroutine(PincerLoop());
                break;
            case BossPatternType.Laser:
                yield return StartCoroutine(LaserLoop());
                break;
        }
    }

    public void StopAllPatterns()
    {
        foreach (Coroutine c in activeCoroutines)
        {
            if (c != null)
                StopCoroutine(c);
        }
        activeCoroutines.Clear();
    }


    void FireBullet(Vector2 direction)
    {
        FireBulletFrom(transform.position, direction);
    }

    void FireBulletFrom(Vector3 position, Vector2 direction)
    {
        GameObject b = ObjectPoolManager.instance.Get(bullet, transform.position, Quaternion.identity);
        EnemyBullet eb = b.GetComponent<EnemyBullet>();
        eb.SetDir(direction);
        EnemySpawner.instance.allEnemyBullet.Add(eb);
        //Debug.Log("발사 방향 : " + direction);
    }


    // 부채꼴
    IEnumerator SectorPatternLoop()
    {
        WaitForSeconds burstShootWait = new WaitForSeconds(0.15f);
        WaitForSeconds cooldownWait = new WaitForSeconds(1.3f);

        while (true)
        {
            int bulletCount = 6;
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
        WaitForSeconds wait = new WaitForSeconds(0.08f);
        WaitForSeconds cooldownWait = new WaitForSeconds(1.5f);

        float rotationOffset = 0f;
        while (true)
        {
            int bulletCount = 15;
            float interval = 360f / bulletCount;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = interval * i + rotationOffset;
                Vector2 direction = Quaternion.Euler(0f, 0f, angle) * Vector2.right;
                FireBullet(direction);
                yield return wait;
            }
            rotationOffset += 15f;
            yield return cooldownWait;
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
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            for (int i = 0; i < armCount; i++)
            {
                float armAngle = angleOffset + (360f / armCount) * i;
                Vector2 direction = Quaternion.Euler(0f, 0f, armAngle) * Vector2.right;
                FireBullet(direction);
            }

            angleOffset += 20f * rotateDir;
            elapsed += 0.2f;

            if (elapsed >= 2.5f)
            {
                rotateDir *= -1f;
                rotateSpeed = Random.Range(15f, 30f);
                elapsed = 0f;
            }
            yield return wait;
        }
    }

    IEnumerator AimedBurstLoop()
    {
        WaitForSeconds shotWait = new WaitForSeconds(0.25f);
        WaitForSeconds cooldownWait = new WaitForSeconds(1.8f);
        while (true)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                Vector2 dir = (playerObj.transform.position - transform.position).normalized;
                for (int i = 0; i < 3; i++)
                {
                    FireBullet(dir);
                    yield return shotWait;
                }
            }
            yield return cooldownWait;
        }
    }

    IEnumerator CrossPatternLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(1.6f);
        float rotationOffset = 0f;
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                float angle = 90f * i + rotationOffset;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
                FireBullet(dir);
            }
            rotationOffset += 10f;
            yield return wait;
        }
    }


    IEnumerator RandomSprayLoop()
    {
        WaitForSeconds burstWait = new WaitForSeconds(0.15f);
        WaitForSeconds cooldownWait = new WaitForSeconds(1.2f);
        while (true)
        {
            int bulletCount = 10;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = Random.Range(0f, 360f);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
                FireBullet(dir);
                yield return burstWait;
            }
            yield return cooldownWait;
        }
    }

    IEnumerator WaveLoop()
    {
        WaitForSeconds shotWait = new WaitForSeconds(0.12f);
        float sweepRange = 70f;
        float sweepSpeed = 90f;
        float angle = -sweepRange;
        float dir = 1f;

        while (true)
        {
            Vector2 fireDir = Quaternion.Euler(0, 0, angle) * Vector2.left;
            FireBullet(fireDir);

            angle += sweepSpeed * dir * 0.12f;
            if (angle >= sweepRange || angle <= -sweepRange)
            {
                dir *= -1f;
            }
            yield return shotWait;
        }
    }


    IEnumerator PincerLoop()
    {
        WaitForSeconds shotWait = new WaitForSeconds(0.3f);
        WaitForSeconds cooldownWait = new WaitForSeconds(1.5f);
        while (true)
        {
            if (leftSpawnPoint != null && rightSpawnPoint != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    FireBulletFrom(leftSpawnPoint.position, Vector2.right);
                    FireBulletFrom(rightSpawnPoint.position, Vector2.left);
                    yield return shotWait;
                }
            }
            yield return cooldownWait;
        }
    }


    IEnumerator LaserLoop()
    {
        WaitForSeconds cooldownWait = new WaitForSeconds(3f);
        while (true)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null && laserWarningPrefab != null)
            {
                Vector2 dir = (playerObj.transform.position - transform.position).normalized;
                GameObject laser = Instantiate(laserWarningPrefab, transform.position, Quaternion.identity);
                laser.GetComponent<BossLaser>().Fire(dir);
            }
            yield return cooldownWait;
        }
    }



}
