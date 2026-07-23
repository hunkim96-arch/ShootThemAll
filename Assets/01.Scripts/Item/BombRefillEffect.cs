using UnityEngine;

public class BombRefillEffect : MonoBehaviour, IItemEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] int amount = 1;

    public void OnPickUp(GameObject player)
    {
        player.GetComponent<PlayerController>().AddBomb(amount);
    }


}
