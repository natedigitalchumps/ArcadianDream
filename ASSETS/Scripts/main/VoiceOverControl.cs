using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceOverControl : MonoBehaviour {

    public List<AudioClip> AClips = new List<AudioClip>();
    public int CurrentClip = 0;
    AudioSource Asource;
    public static VoiceOverControl instant;
    public AudioMixer MainMixer;

    void Awake()
    {
        if (instant == null)
            instant = this;
        Asource = GetComponent<AudioSource>();
    }

    public void PlayTheRightAudio()
    {
        float audiolevel=0f;
        float currentlevel=0f;
        MainMixer.GetFloat("backgroundVolume", out currentlevel);
        audiolevel = currentlevel;
        currentlevel -= 20;
        MainMixer.SetFloat("backgroundVolume", currentlevel);
        Asource.PlayOneShot(AClips[CurrentClip]);
        StartCoroutine(AudioChecker(audiolevel));
    }

IEnumerator AudioChecker(float level)
    {
        while(Asource.isPlaying)
        {
            yield return new WaitForSeconds(.01f);
        }
        MainMixer.SetFloat("backgroundVolume", level);
    }
}
