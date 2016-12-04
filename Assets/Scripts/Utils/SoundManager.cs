using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{


    public int timingPhase1;
    public int timingPhase2;
    public int timingPhase3;

    public AudioClip[] clips;


    static private AudioSource audio;

    void Start()
    {
        audio = this.gameObject.GetComponent<AudioSource>();


        PlaySound("Phase_1", timingPhase1);
        PlaySound("Phase_2", timingPhase2);
        PlaySound("Phase_3", timingPhase3);
    }



    public void PlaySound(string name, float delay = 0f)
    {

        StartCoroutine(playSound(name, delay));


        //foreach (AudioClip clip in clips)
        //{
        //    if (clip.name == name)
        //    {
        //        audio.PlayOneShot(clip);
        //    }
        //}
    }


    IEnumerator playSound(string name, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                audio.PlayOneShot(clip);
            }
        }
    }

}
