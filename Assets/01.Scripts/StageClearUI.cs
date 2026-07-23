using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Transform playerStartPosition;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(long score)
    {
        panel.SetActive(true);
        scoreText.text = score.ToString();
        Time.timeScale = 0f;
    }


    public void OnClickNextStage()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);

        ObjectPoolManager.instance.ReturnAllActive();
        EnemySpawner.instance.ClearAllForStageObjects();

        GameObject player = GameObject.Find("Player");
        if (player != null && playerStartPosition != null)
        {
            player.transform.position = playerStartPosition.position;
        }

        StageManager.instance.GoToNextStage();
    }


    public void OnClickLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}