﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum POOLED_OBJECTS
{
    COMMIT,
    PLONGEUR,
    GOURMAND,
    GORDON,
    BULLET,
}




public class ObjectPool : Singleton<ObjectPool>
{

    
    public GameObject commitPrefab;
    public int commitAmount = 20;
    private List<GameObject> commitObjects;

    public GameObject plongeurPrefab;
    public int plongeurAmount = 20;
    private List<GameObject> plongeurObjects;

    public GameObject gourmandPrefab;
    public int gourmandAmount = 20;
    private List<GameObject> gourmandObjects;

    public GameObject gordonPrefab;
    public int gordonAmount = 20;
    private List<GameObject> gordonObjects;

    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    private List<GameObject> bulletObjects;


    void Awake()
    {
        commitObjects = new List<GameObject>();
        for (int i = 0; i < commitAmount; ++i)
        {
            GameObject obj = Instantiate(commitPrefab);
            obj.SetActive(false);
            commitObjects.Add(obj);
        }
        plongeurObjects = new List<GameObject>();
        for (int i = 0; i < plongeurAmount; ++i)
        {
            GameObject obj = Instantiate(plongeurPrefab);
            obj.SetActive(false);
            plongeurObjects.Add(obj);
        }
        gourmandObjects = new List<GameObject>();
        for (int i = 0; i < gourmandAmount; ++i)
        {
            GameObject obj = Instantiate(gourmandPrefab);
            obj.SetActive(false);
            gourmandObjects.Add(obj);
        }
        gordonObjects = new List<GameObject>();
        for (int i = 0; i < gordonAmount; ++i)
        {
            GameObject obj = Instantiate(gordonPrefab);
            obj.SetActive(false);
            gordonObjects.Add(obj);
        }
        bulletObjects = new List<GameObject>();
        for (int i = 0; i < bulletAmount; ++i)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletObjects.Add(obj);
        }
    }

    


    public GameObject GetPooledObject(POOLED_OBJECTS type)
    {
        GameObject obj;
        switch (type)
        {
            case POOLED_OBJECTS.COMMIT:
                for (int i = 0; i < commitObjects.Count; ++i)
                {
                    if (!commitObjects[i].activeInHierarchy)
                    {
                        return commitObjects[i];
                    }
                }

                obj = Instantiate(commitPrefab);
                commitObjects.Add(obj);
                return obj;
            case POOLED_OBJECTS.PLONGEUR:
                for (int i = 0; i < plongeurObjects.Count; ++i)
                {
                    if (!plongeurObjects[i].activeInHierarchy)
                    {
                        return plongeurObjects[i];
                    }
                }

                obj = Instantiate(plongeurPrefab);
                plongeurObjects.Add(obj);
                return obj;
            case POOLED_OBJECTS.GOURMAND:
                for (int i = 0; i < gourmandObjects.Count; ++i)
                {
                    if (!gourmandObjects[i].activeInHierarchy)
                    {
                        return gourmandObjects[i];
                    }
                }

                obj = Instantiate(gourmandPrefab);
                gourmandObjects.Add(obj);
                return obj;
            case POOLED_OBJECTS.GORDON:
                for (int i = 0; i < gordonObjects.Count; ++i)
                {
                    if (!gordonObjects[i].activeInHierarchy)
                    {
                        return gordonObjects[i];
                    }
                }

                obj = Instantiate(gordonPrefab);
                gordonObjects.Add(obj);
                return obj;
            case POOLED_OBJECTS.BULLET:
                for (int i = 0; i < bulletObjects.Count; ++i)
                {
                    if (!bulletObjects[i].activeInHierarchy)
                    {
                        return bulletObjects[i];
                    }
                }

                obj = Instantiate(bulletPrefab);
                bulletObjects.Add(obj);
                return obj;
        }
        return null;
    }

}