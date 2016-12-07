using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;


public class LogoController : MonoBehaviour {


    List<Sprite> logoSprites;
    Image logo;
    int current = 0;

	// Use this for initialization
	void Awake ()
    { 
        current = 0;
        logo = GetComponent<Image>();
        logoSprites = new List<Sprite>();
        
        for (int i = 0; i < 64; ++i)
        {
            if (i < 10)
            {
                logoSprites.Add(Resources.Load<Sprite>("LOGO/Logo-title_0000" + i));
            }
            else
            {
                logoSprites.Add(Resources.Load<Sprite>("LOGO/Logo-title_000" + i));
            }
        }
        logo.sprite = logoSprites[0];
        StartCoroutine(PlayLogoAnimation());
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    IEnumerator PlayLogoAnimation()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        foreach (Sprite s in logoSprites)
        {
            logo.sprite = s;
            yield return wait;
        }
    }


}
