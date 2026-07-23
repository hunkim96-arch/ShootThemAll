using UnityEngine;

public class ScoreBonusEffect : MonoBehaviour, IItemEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] int scoreAmount = 300;

    public void OnPickUp(GameObject player)
    {
        GameManager.instance.GetScore(scoreAmount);
    }

}
