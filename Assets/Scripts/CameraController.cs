using UnityEngine;
using System.Collections;



public class CameraController : MonoBehaviour
{

    public Transform player1;
    public Transform player2;

    public float zoomOutMaxValue;
    public float zoomInMaxValue;
    public float playerAndEdgeSpace = 5f;

    public bool zoomMaxed = false;

    private Camera camera;
    
    Vector3 cameraFocus;


    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate ()
    {
       // cameraFocus = player1.position;
       //// cameraFocus.z = camera.transform.position.z;
       
       //// transform.position = Vector3.Lerp(transform.position, cameraFocus,.5f);

       // //camera.transform.position;
       // //Vector3 newZoomValue = new Vector3(
       // //    camera.transform.position.x,
       // //    camera.transform.position.y,
       // //    -((Mathf.Abs(player1.position.x - player2.position.x) + playerAndEdgeSpace*2) / 2));
       // //float newZ = -((Mathf.Abs(player1.position.x - player2.position.x) + playerAndEdgeSpace * 2) / 2);
       // //if (newZ < zoomOutMaxValue)
       // //{
       // //    newZ = zoomOutMaxValue;
       // //    zoomMaxed = true;
       // //}
       // //else
       // //{
       // //    zoomMaxed = false;
       // //}
        

       // camera.transform.position = new Vector3(
       //     camera.transform.position.x,
       //     camera.transform.position.y,
       //     newZ);

    }

}
