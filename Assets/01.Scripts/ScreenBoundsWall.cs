using UnityEngine;

public class ScreenBoundsWall : MonoBehaviour
{

    [SerializeField] float margin = 0f;
    BoxCollider2D screenWall;


    void Start()
    {
        screenWall = GetComponent<BoxCollider2D>();
        SetBounds();
    }


    void SetBounds()
    {
        Camera cam = Camera.main;
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 size = new Vector2((max.x - min.x) + margin * 2f, (max.y - min.y) + margin * 2f);
        Vector2 center = new Vector2((min.x + max.x) / 2f, (min.y + max.y) / 2f);

        transform.position = center;
        screenWall.size = size;
        screenWall.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return;
        }
        Debug.Log("Return" + collision.gameObject.name + " / InstanceID " + collision.gameObject.GetInstanceID());
        ObjectPoolManager.instance.Return(collision.gameObject);
    }


}
