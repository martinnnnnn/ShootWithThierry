﻿using UnityEngine;
using System.Collections;



public class CameraManager : MonoBehaviour
{

    public Transform hero;
    private Camera camera;

    Vector3 cameraFocus;


    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        cameraFocus = hero.position;
        cameraFocus.z = -100;

        transform.position = Vector3.Lerp(transform.position, cameraFocus, 1);

        //camera.transform.position;
        //Vector3 newZoomValue = new Vector3(
        //    camera.transform.position.x,
        //    camera.transform.position.y,
        //    -((Mathf.Abs(player1.position.x - player2.position.x) + playerAndEdgeSpace * 2) / 2));
        //float newZ = -((Mathf.Abs(player1.position.x - player2.position.x) + playerAndEdgeSpace * 2) / 2);
        //if (newZ < zoomOutMaxValue)
        //{
        //    newZ = zoomOutMaxValue;
        //    zoomMaxed = true;
        //}
        //else
        //{
        //    zoomMaxed = false;
        //}


        //camera.transform.position = new Vector3(
        //    camera.transform.position.x,
        //    camera.transform.position.y,
        //    newZ);

    }

}
