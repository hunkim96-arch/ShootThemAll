using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 점수
    // 플레이어 목숨

    PlayerController player;

    private long score;

    [SerializeField] Transform revivePostion;
    [SerializeField] int lifeCount=3;
    [SerializeField] int maxLifeCount = 3;

    [SerializeField] GameOverUI gameOverUI;
    [SerializeField] StageClearUI stageClearUI;

    int nowLife;

    bool isGameOver = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        nowLife = lifeCount;
    }

    public void ReducePlayerLife()
    {
        if (isGameOver) return;

        if(nowLife>0)
        {
            nowLife--;
            RessurrectionPlayer();
            UIManager.instance.SetLifeText(nowLife.ToString());

            // 플레이어 부활
            // 플레이어 초기화(무기레벨 등)
            // 플레이어 잠시 무적 처리
        }
        else
        {
            isGameOver = true;
            EnemySpawner.instance.StopSpawning();
            player.gameObject.SetActive(false);
            gameOverUI.Show(score);
            // 이어하기? 
        }
    }

    public void AddLife()
    {
        nowLife++;
        nowLife = Mathf.Clamp(nowLife, 0, maxLifeCount);
        UIManager.instance.SetLifeText(nowLife.ToString());
    }


    public void TriggerPlayerInvincible(float duration)
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            playerObj.GetComponent<PlayerController>().SetInvincible(duration);
        }
    }


    private void RessurrectionPlayer()
    {
        // 플레이어 부활 및 위치 조정, 초기화
        player.transform.position = revivePostion.position;
        player.ResetPlayer();
    }


    public void GetScore(int s)
    {
        score += s;
        UIManager.instance.SetScoreText(score.ToString());
    }

    public void ShowStageClear()
    {
        stageClearUI.Show(score);
    }

    public void BossDefeated()
    {
        stageClearUI.Show(score);
    }

}
