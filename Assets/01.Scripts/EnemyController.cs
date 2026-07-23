using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHp = 3;
    [SerializeField] int nowHp;
    [SerializeField] int scoreValue = 100;
    [SerializeField] List<GameObject> itemPrefabs = new List<GameObject>();
    [SerializeField] float itemDropChance = 0.2f;

    void OnEnable()
    {
        nowHp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        nowHp -= dmg;
        if (nowHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
        if (itemPrefabs.Count > 0 && Random.value <= itemDropChance)
        {
            int index = Random.Range(0, itemPrefabs.Count);
            Instantiate(itemPrefabs[index], transform.position, Quaternion.identity);
        }

        GameManager.instance.GetScore(scoreValue);
        EnemySpawner.instance.OnEnemyKilled();
        ObjectPoolManager.instance.Return(gameObject);
    }

    void OnDisable()
    {
        if (EnemySpawner.instance != null)
            EnemySpawner.instance.allEnemy.Remove(this);
    }


}