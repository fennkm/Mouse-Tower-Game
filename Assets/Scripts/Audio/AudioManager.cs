using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sfx;
    [SerializeField] AudioClip musicStart;
    [SerializeField] AudioClip musicLoop;
    [SerializeField, Range(0, 1)] float sfxVolume = 1f;
    [SerializeField, Range(0, 1)] float musicVolume = 1f;
    private AudioSource sfxPlayer;
    private AudioSource musicStartPlayer;
    private AudioSource musicLoopPlayer;
    private Dictionary<string, AudioClip> sfxDict;
    // Start is called before the first frame update
    void Start()
    {
        sfxPlayer = GetComponents<AudioSource>()[0];
        musicStartPlayer = GetComponents<AudioSource>()[1];
        musicLoopPlayer = GetComponents<AudioSource>()[2];

        sfxDict = new();

        foreach (AudioClip clip in sfx)
            sfxDict.Add(clip.name, clip);

        musicStartPlayer.clip = musicStart;
        musicStartPlayer.PlayDelayed(.1f);

        musicLoopPlayer.clip = musicLoop;
        musicLoopPlayer.loop = true;
        musicLoopPlayer.PlayDelayed(.1f + musicStart.length);
    }

    // Update is called once per frame
    void Update()
    {
        sfxPlayer.volume = sfxVolume;
        musicStartPlayer.volume = musicVolume;
        musicLoopPlayer.volume = musicVolume;
    }

    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
            sfxPlayer.PlayOneShot(sfxDict[name]);
        else
            throw new ArgumentException(name + " in not a valid SFX file!");
    }
}
