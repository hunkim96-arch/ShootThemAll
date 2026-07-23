using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField] List<GameObject> stage1Enemies = new List<GameObject>();
    [SerializeField] List<GameObject> stage2Enemies = new List<GameObject>();
    [SerializeField] List<GameObject> stage3Enemies = new List<GameObject>();

    [SerializeField] int stage1KillCount = 20;
    [SerializeField] int stage2KillCount = 30;
    [SerializeField] int stage3KillCount = 40;

    [SerializeField] GameObject stage1Boss;
    [SerializeField] GameObject stage2Boss;
    [SerializeField] GameObject stage3Boss;

    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] StageClearUI stageClearUI;
    [SerializeField] VictoryUI victoryUI;

    int currentStageIndex = 0;
    int totalStageCount = 3;
    long score;

    void Awake()
    {
        instance = this;
    }

    public List<GameObject> GetCurrentEnemyPrefabs()
    {
        if (currentStageIndex == 0)
        {
            return stage1Enemies;
        }
        if (currentStageIndex == 1)
        {
            return stage2Enemies;
        }
        return stage3Enemies;
    }

    public int GetCurrentKillCount()
    {
        if (currentStageIndex == 0)
        {
            return stage1KillCount;
        }
        if (currentStageIndex == 1)
        {
            return stage2KillCount;
        }
        return stage3KillCount;
    }

    public GameObject GetCurrentBoss()
    {
        if (currentStageIndex == 0)
        {
            return stage1Boss;
        }
        if (currentStageIndex == 1)
        {
            return stage2Boss;
        }
        return stage3Boss;
    }

    public bool HasNextStage()
    {
        return currentStageIndex < totalStageCount - 1;
    }

    public void GoToNextStage()
    {
        currentStageIndex++;
        EnemySpawner.instance.StartNewStage();
    }

    public int GetStageNumber()
    {
        return currentStageIndex + 1;
    }

}