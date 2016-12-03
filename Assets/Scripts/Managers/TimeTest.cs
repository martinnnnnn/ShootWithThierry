using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class TimeTest : Singleton<TimeTest>
{


    void Awake()
    {
        StartCoroutine(LoadMainLevel());
    }

    IEnumerator LoadMainLevel()
    {
        SceneManager.UnloadScene("test1");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("test1");
    }
}

