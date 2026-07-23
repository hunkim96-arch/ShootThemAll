using UnityEngine;

public class ExtraLifeEffect : MonoBehaviour, IItemEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnPickUp(GameObject player)
    {
        GameManager.instance.AddLife();
    }

}
