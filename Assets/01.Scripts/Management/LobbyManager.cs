using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }


    public void MoveGameScene()
    {
        SoundManager.instance.PlaySFX("UI_Button_Click");
        SceneManager.LoadScene("GameScene");
    }

}
