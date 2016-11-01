using UnityEngine;
using System.Collections;

public class Arms : MonoBehaviour
{

    Vector3 mousePosition;
    public ObjectPool bulletPool;

    public Sprite ammoSprite;

    void Update()
    {
        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = bulletPool.GetPooledObject();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<SpriteRenderer>().sprite = ammoSprite;
            bullet.SetActive(true);
        }

    }

}
