using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public List<EnemyController> allEnemy = new List<EnemyController>();
    public List<EnemyBullet> allEnemyBullet = new List<EnemyBullet>();
    [SerializeField] List<Transform> spawnPosition = new List<Transform>();
    [SerializeField] Transform bossSpawnPosition;

    List<GameObject> currentEnemyPrefabs;
    int killCountForBoss;
    GameObject bossPrefab;
    int killCount = 0;
    bool bossSpawned = false;
    Coroutine spawnCoroutine;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartNewStage();
    }

    public void StartNewStage()
    {
        currentEnemyPrefabs = StageManager.instance.GetCurrentEnemyPrefabs();
        killCountForBoss = StageManager.instance.GetCurrentKillCount();
        bossPrefab = StageManager.instance.GetCurrentBoss();
        killCount = 0;
        bossSpawned = false;

        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);

        while (true)
        {
            int posIndex = Random.Range(0, spawnPosition.Count);
            int prefabIndex = Random.Range(0, currentEnemyPrefabs.Count);

            GameObject obj = ObjectPoolManager.instance.Get(currentEnemyPrefabs[prefabIndex], spawnPosition[posIndex].position, Quaternion.identity);
            allEnemy.Add(obj.GetComponent<EnemyController>());

            yield return wait;
        }
    }

    public void OnEnemyKilled()
    {
        killCount++;

        if (!bossSpawned && killCount >= killCountForBoss)
        {
            bossSpawned = true;
            StopSpawning();
            Instantiate(bossPrefab, bossSpawnPosition.position, Quaternion.identity);
        }
    }

    public void ClearEnemyBullet()
    {
        for (int i = 0; i < allEnemyBullet.Count; i++)
        {
            if (allEnemyBullet[i] != null)
                ObjectPoolManager.instance.Return(allEnemyBullet[i].gameObject);
        }

        allEnemyBullet.Clear();
    }

    public void TakeDamageAllEnemy(int dmg)
    {
        for (int i = allEnemy.Count - 1; i >= 0; i--)
        {
            allEnemy[i].TakeDamage(dmg);
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    
    public void ClearAllForStageObjects()
    {
        allEnemy.Clear();
        allEnemyBullet.Clear();
    }


}