using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI titleText;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(long score)
    {
        panel.SetActive(true);
        titleText.text = "GAME OVER";
        RankingManager.AddScore((int)score);
        Time.timeScale = 0f;
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PlaySFX("UI_Button_Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickLobby()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PlaySFX("UI_Button_Click");
        SceneManager.LoadScene("LobbyScene");
    }
}