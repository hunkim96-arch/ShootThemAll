using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI rankingText;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(long score)
    {
        panel.SetActive(true);
        scoreText.text = score.ToString();
        RankingManager.AddScore((int)score);
        rankingText.text = RankingManager.GetRankingText();
        Time.timeScale = 0f;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}