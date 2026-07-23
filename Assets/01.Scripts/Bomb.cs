using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] int damage = 5;
    [SerializeField] float delayTime;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask enemyBulletLayer;

    
    public void UseBomb()
    {

        Camera cam = Camera.main;
        Vector3 min = cam.ViewportToWorldPoint(Vector3.zero);
        Vector3 max = cam.ViewportToWorldPoint(Vector3.one);


        for (int i = EnemySpawner.instance.allEnemy.Count - 1; i >= 0; i--)
        {
            EnemyController enemy = EnemySpawner.instance.allEnemy[i];
            if (enemy == null) continue;

            Vector3 pos = enemy.transform.position;
            bool isOnScreen = pos.x >= min.x && pos.x <= max.x && pos.y >= min.y && pos.y <= max.y;

            if (isOnScreen)
            {
                enemy.TakeDamage(damage);
            }
            EnemySpawner.instance.ClearEnemyBullet();
        }


    }
}