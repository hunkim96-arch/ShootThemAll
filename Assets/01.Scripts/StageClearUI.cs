using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Transform playerStartPosition;

    void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(long score)
    {
        panel.SetActive(true);
        titleText.text = "STAGE CLEAR";
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

        SoundManager.instance.PlaySFX("UI_Button_Click");
        StageManager.instance.GoToNextStage();
    }


    public void OnClickLobby()
    {
        SoundManager.instance.PlaySFX("UI_Button_Click");
        SceneManager.LoadScene("LobbyScene");
    }
}