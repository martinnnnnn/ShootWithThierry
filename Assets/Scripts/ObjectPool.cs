using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : Singleton<ObjectPool>
{

    public GameObject objectType;
    public int pooledAmount = 20;
    

    List<GameObject> pooledObjects;


    public void InitPool(GameObject prefab, int initialSize)
    {
        objectType = prefab;
        pooledAmount = initialSize;

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; ++i)
        {
            GameObject obj = Instantiate(objectType);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; ++i)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject obj = Instantiate(objectType);
        pooledObjects.Add(obj);
        return obj;
    }


    //void OnAwake()
    //{
    //    pooledObjects = new List<GameObject>();
    //    for (int i = 0; i < pooledAmount; ++i)
    //    {
    //        GameObject obj = Instantiate(objectType);
    //        obj.SetActive(false);
    //        pooledObjects.Add(obj);
    //    }
    //}

}
