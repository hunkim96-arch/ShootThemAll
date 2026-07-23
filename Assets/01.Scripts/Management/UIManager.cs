using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI bombText;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }
    public void SetLifeText(string text)
    {
        lifeText.text = text;
    }
    public void SetBombText(string text)
    {
        bombText.text = text;
    }




}
