using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    float moveSpeed;



    private void Start()
    {
        moveSpeed = 3f;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerAttack>().WeaponLevelup();
            Destroy(gameObject);
        }
    }



}
