using UnityEngine;
using System.Collections;



public class CameraManager : MonoBehaviour
{

    public Transform hero;
    private Camera camera;

   // Vector3 cameraFocus;


    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
       // cameraFocus = hero.position;
        //cameraFocus.z = camera.transform.position.z;

        transform.position = new Vector3(hero.position.x, hero.position.y, -10);

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
