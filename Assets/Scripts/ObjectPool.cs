using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{

    public GameObject objectType;
    public int pooledAmount = 20;

    List<GameObject> pooledObjects;
    

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; ++i)
        {
            GameObject obj = Instantiate(objectType);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    void Update()
    {
        //Debug.Log(pooledObjects[1].GetComponent<Rigidbody>().velocity);
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
        
        GameObject obj = (GameObject)Instantiate(objectType);
        pooledObjects.Add(obj);
        return obj;
    }

    
    
}
