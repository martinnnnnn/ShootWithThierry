﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TheButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire3"))
        {
            SceneManager.LoadScene("test1");
        }

    }

    public void OnClickOnTheButton()
    {
        SceneManager.LoadScene("test1");
    }
}
