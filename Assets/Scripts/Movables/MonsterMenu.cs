using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;





public class MonsterMenu : MonoBehaviour
{
    private int MonsterLife;

    public Animator anim;

    void Awake()
    {
        MonsterLife = 200;

        anim.SetFloat("Life", MonsterLife);
    }

 
}
