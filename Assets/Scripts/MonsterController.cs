using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class MonsterController : MonoBehaviour
{

    public int life = 100;
    public int firstStage = 80;
    public int secondStage = 50;
    public int thirdSage = 20;

    private List<Transform> firstStagePlaces;
    public GameObject lava;
    private bool firstStagePlayed = false;
    private bool secondStagePlayed = false;
    private bool thirdStagePlayed = false;

    void Start()
    {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            firstStagePlaces.Add(t);
        }
    }

    void Update()
    {
        if (life < firstStage && !firstStagePlayed)
        {
            firstStagePlayed = true;
            foreach (Transform t in firstStagePlaces)
            {
                GameObject go = Instantiate(lava, t) as GameObject;
            }
        }
        
    }

    public void LoseLife(int damage)
    {
        --life;
    }

}
