using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class BossController : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHp = 50;
    [SerializeField] BossAttack bossAttack;
    [SerializeField] GameObject item;
    [SerializeField] int scoreValue = 5000;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float enterSpeed = 2f;
    [SerializeField] float clearDelay = 3f;

    int nowHp;
    int currentPhase = 0;
    bool phase2Started = false;
    bool phase3Started = false;
    bool phaseLocked = false;
    bool isEntering = true;
    bool isDead;

    void Start()
    {
        nowHp = maxHp;
    }

    void Update()
    {
        if (isEntering)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enterSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isEntering = false;
                bossAttack.StartPhase(0);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        nowHp -= dmg;
        if (nowHp < 0) nowHp = 0;

        float hpRatio = (float)nowHp / maxHp;

        if (!phase2Started && hpRatio <= 0.66f)
        {
            phase2Started = true;
            bossAttack.StartPhase(1);
        }
        else if (!phase3Started && hpRatio <= 0.33f)
        {
            phase3Started = true;
            bossAttack.StartPhase(2);
        }

        if (nowHp <= 0)
        {
            Die();
        }
    }


    IEnumerator PhaseLockRoutine()
    {
        phaseLocked = true;
        yield return new WaitForSeconds(2f);
        phaseLocked = false;
    }

    void Die()
    {
        isDead = true;
        bossAttack.StopAllPatterns();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        Instantiate(item, transform.position, Quaternion.identity);
        GameManager.instance.GetScore(scoreValue);
        GameManager.instance.TriggerPlayerInvincible(clearDelay);

        if (StageManager.instance.HasNextStage())
        {
            GameManager.instance.ShowStageClear();
        }
        else
        {
            GameManager.instance.BossDefeated();
        }
        Destroy(gameObject);
    }

    IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSeconds(clearDelay);
        if (StageManager.instance.HasNextStage())
        {
            GameManager.instance.ShowStageClear();
        }
        else
        {
            GameManager.instance.BossDefeated();
        }
        Destroy(gameObject);
    }


}