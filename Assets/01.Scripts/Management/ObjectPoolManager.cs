using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{


    public static ObjectPoolManager instance;
    Dictionary<GameObject, Queue<GameObject>> poolDic = new Dictionary<GameObject, Queue<GameObject>>();
    List<GameObject> activeObjects = new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }


    public GameObject Get(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (!poolDic.ContainsKey(prefab))
        {
            poolDic[prefab] = new Queue<GameObject>();

        }

        GameObject obj = null;
        while (poolDic[prefab].Count > 0)
        {
            obj = poolDic[prefab].Dequeue();
            if (obj != null)
                break;
        }

        if (obj != null)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);
        }

        else
        {
            obj = Instantiate(prefab, pos, rot);
            obj.AddComponent<PooledObject>().prefab = prefab;
        }

        activeObjects.Add(obj);
        Debug.Log("Get 호출" + obj.name + " / 현재 activeObjects 개수" + activeObjects.Count);
        return obj;
    }


    public void Return(GameObject obj)
    {
        if (obj == null) return;
        if (!obj.activeSelf) return;


        PooledObject pooled = obj.GetComponent<PooledObject>();
        if (pooled == null)
        {
            Destroy(obj);
            return;
        }
        obj.SetActive(false);
        poolDic[pooled.prefab].Enqueue(obj);
        activeObjects.Remove(obj);
    }

    public void ReturnAllActive()
    {
        Debug.Log("반환" + activeObjects.Count);
        for (int i = activeObjects.Count - 1; i >= 0; i--)
        {
            if (activeObjects[i] == null)
            {
                activeObjects.RemoveAt(i);
                continue;
            }
            Debug.Log("반환 시도" + activeObjects[i].name);
            Return(activeObjects[i]);
        }
    }

}
