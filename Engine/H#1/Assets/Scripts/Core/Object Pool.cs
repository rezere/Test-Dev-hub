using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private int poolSize = 10;

    [SerializeField]
    private Queue<GameObject> pool = new Queue<GameObject>();
    private GameObject currentPrefab;

    public void SetPrefab(GameObject prefab)
    {
        if(currentPrefab != prefab)
        {
            ClearPool();
            currentPrefab = prefab;
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(currentPrefab);

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    private void ClearPool()
    {
        foreach (var item in pool)
        {
            Destroy(item.gameObject);
        }
        pool.Clear();
    }
}
