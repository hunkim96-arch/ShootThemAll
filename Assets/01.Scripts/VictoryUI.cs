using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{

    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI scoreText;


    void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(long score)
    {
        panel.SetActive (true);
        scoreText.text = score.ToString();
    }

    public void OnClickLobby()
    {
        Time.timeScale = 1f;
        SoundManager.instance.PlaySFX("UI_Button_Click");
        SceneManager.LoadScene("LobbyScene");
    }

}
