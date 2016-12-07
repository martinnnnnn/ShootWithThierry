using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{


    public int timingPhase1;
    public int timingPhase2;
    public int timingPhase3;

    public AudioClip[] clips;


    private AudioSource mainAudio;
    private AudioSource general;
    private AudioSource monsterPhase2Audio;
    private AudioSource monsterPhase3Audio;

    void Awake()
    {
        mainAudio = this.gameObject.GetComponent<AudioSource>();
        monsterPhase2Audio = gameObject.AddComponent<AudioSource>();
        monsterPhase3Audio = gameObject.AddComponent<AudioSource>();
        general = gameObject.AddComponent<AudioSource>();
        StartGameMusic();
    }


    public void StartGameMusic()
    {
        PlaySound("Phase_1", timingPhase1);
        PlaySound("Phase_2", timingPhase2);
        PlaySound("Phase_3", timingPhase3);

        //PlaySound("General");
        //PlaySound("Phase3_Loop", GetAudioClip("General").length);

        AudioClip General = null;
        AudioClip Phase3_Loop = null;
        AudioClip Flan_Etat_2_Debut = null;
        AudioClip Flan_Etat_2_Loop = null;
        AudioClip Flan_Etat_3_Debut = null;
        AudioClip Flan_Etat_3_Loop = null;

        foreach (AudioClip clip in clips)
        {
            if (clip.name == "General")
            {
                General = clip;
            }
            if (clip.name == "Phase3_Loop")
            {
                Phase3_Loop = clip;
            }
            if (clip.name == "Flan_Etat2_Debut")
            {
                Flan_Etat_2_Debut = clip;
            }
            if (clip.name == "Flan_Etat2_Loop")
            {
                Flan_Etat_2_Loop = clip;
            }
            if (clip.name == "Flan_Etat3_Debut")
            {
                Flan_Etat_3_Debut = clip;
            }
            if (clip.name == "Flan_Etat3_Loop")
            {
                Flan_Etat_3_Loop = clip;
            }
        }
        StartCoroutine(playSound(general, General));
        StartCoroutine(playSound(general, Phase3_Loop,General.length,true));

        StartCoroutine(playSound(monsterPhase2Audio, Flan_Etat_2_Debut));
        StartCoroutine(playSound(monsterPhase2Audio, Flan_Etat_2_Loop, Flan_Etat_2_Debut.length, true));
        monsterPhase2Audio.mute = true;
        StartCoroutine(playSound(monsterPhase3Audio, Flan_Etat_3_Debut));
        StartCoroutine(playSound(monsterPhase2Audio, Flan_Etat_3_Loop, Flan_Etat_3_Debut.length, true));
        monsterPhase3Audio.mute = true;

    }

    public void MuteMonsterStage(string name)
    {
        if (name == "MonsterPhase2")
        {
            monsterPhase2Audio.mute = true;
        }
        if (name == "MonsterPhase3")
        {
            monsterPhase3Audio.mute = true;
        }
    }

    public void UnmuteMonsterStage(string name)
    {
        if (name == "MonsterPhase2")
        {
            monsterPhase2Audio.mute = false;
        }
        if (name == "MonsterPhase3")
        {
            monsterPhase3Audio.mute = false;
        }
    }


    public AudioClip GetAudioClip(string name)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
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
                mainAudio.PlayOneShot(clip);
            }
        }
    }

    IEnumerator playSound(AudioSource source, AudioClip clip, float delay = 0f, bool loop = false)
    {
        yield return new WaitForSeconds(delay);

        source.loop = loop;
        source.PlayOneShot(clip);
    }

}
