using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DeapthManager : Singleton<DeapthManager>
{

    List<GameObject> actors;


    void Awake()
    {
        actors = new List<GameObject>();
    }

    void LateUpdate()
    {
        actors = actors.OrderBy(o => o.transform.position.y).ToList();
        foreach (GameObject o in actors)
        {
            Vector3 newPosition = new Vector3(o.transform.position.x, o.transform.position.y, o.transform.position.y);
            o.transform.position = newPosition;
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    for (int i = 0; i < actors.Count; ++i)
        //    {
        //        Debug.Log(i + ":" + actors[i].transform.position.y);
        //    }
        //}
    }



    public void AddActor(GameObject o)
    {
        actors.Add(o);
    }

    public void RemoveActor(GameObject o)
    {
        actors.Remove(o);
    }
}


