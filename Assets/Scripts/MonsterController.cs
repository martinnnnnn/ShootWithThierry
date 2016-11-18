using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class MonsterController : MonoBehaviour
{
    public int firstStage = 80;
    public int secondStage = 50;
    public int thirdSage = 20;

    private MortalCharacter myLife;

    private List<Transform> firstStagePlaces;
    public GameObject lava;
    private bool firstStagePlayed = false;
    private bool secondStagePlayed = false;
    private bool thirdStagePlayed = false;

    void Start()
    {
        myLife = GetComponent<MortalCharacter>();
        firstStagePlaces = new List<Transform>();
        foreach (Transform t in transform)
        {
            firstStagePlaces.Add(t);
        }
    }

    void Update()
    {
        if (myLife.life < firstStage && !firstStagePlayed)
        {
            firstStagePlayed = true;
            foreach (Transform t in firstStagePlaces)
            {
                GameObject go = Instantiate(lava, t.position,t.rotation) as GameObject;
            }
        }

    }

}
