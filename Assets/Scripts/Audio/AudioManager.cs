using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private AudioClip musicStart;
    [SerializeField] private AudioClip musicLoop;
    [SerializeField] private AudioClip menuLoop;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float gameMusicWindup;
    private AudioSource sfxPlayer;
    private AudioSource musicStartPlayer;
    private AudioSource musicLoopPlayer;
    private Dictionary<string, AudioClip> sfxDict;
    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    private bool musicPlaying;

    void Awake()
    {
       musicPlaying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        sfxPlayer = GetComponents<AudioSource>()[0];
        musicStartPlayer = GetComponents<AudioSource>()[1];
        musicLoopPlayer = GetComponents<AudioSource>()[2];

        sfxDict = new();

        foreach (AudioClip clip in sfx)
            sfxDict.Add(clip.name, clip);

        
        musicLoopPlayer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
            sfxPlayer.PlayOneShot(sfxDict[name]);
        else
            throw new ArgumentException(name + " in not a valid SFX file!");
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;

        SetSFXVolume(sfxVolume);
        SetMusicVolume(musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        sfxPlayer.volume = sfxVolume * masterVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;

        musicStartPlayer.volume = musicVolume * masterVolume;
        musicLoopPlayer.volume = musicVolume * masterVolume;
    }

    public void FadeMusicOut()
    {
        if (musicPlaying) StartCoroutine("FadeOut");
    }

    private IEnumerator FadeOut()
    {
        musicPlaying = false;

        float vol = 1f;

        while (vol > 0)
        {            
            vol -= 1f * Time.deltaTime / fadeDuration;
            musicLoopPlayer.volume = musicVolume * masterVolume * vol;

            yield return null;
        }

        musicLoopPlayer.Stop();
        musicLoopPlayer.volume = musicVolume * masterVolume;
    }

    private IEnumerator FadeToGameMusic()
    {
        musicPlaying = false;

        float vol = 1f;

        while (vol > 0)
        {            
            vol -= 1f * Time.deltaTime / gameMusicWindup;
            musicLoopPlayer.volume = musicVolume * masterVolume * vol;

            yield return null;
        }

        musicStartPlayer.Stop();
        musicLoopPlayer.Stop();

        musicStartPlayer.clip = musicStart;
        musicLoopPlayer.clip = musicLoop;

        musicStartPlayer.volume = musicVolume * masterVolume;
        musicLoopPlayer.volume = musicVolume * masterVolume;

        musicStartPlayer.Play();
        musicLoopPlayer.PlayDelayed(musicStart.length);

        musicPlaying = true;
    }

    private IEnumerator FadeToMenuMusic()
    {
        musicPlaying = false;

        float vol = 1f;

        while (vol > 0)
        {            
            vol -= 1f * Time.deltaTime / fadeDuration;
            musicLoopPlayer.volume = musicVolume * masterVolume * vol;

            yield return null;
        }

        musicStartPlayer.Stop();
        musicLoopPlayer.Stop();

        musicLoopPlayer.clip = menuLoop;
        
        musicStartPlayer.volume = musicVolume * masterVolume;
        musicLoopPlayer.volume = musicVolume * masterVolume;

        musicLoopPlayer.Play();

        musicPlaying = true;
    }

    public void StartGameMusic()
    {
        if (musicPlaying) 
            StartCoroutine("FadeToGameMusic");
        else
        {
            StopAllCoroutines();

            musicStartPlayer.volume = musicVolume * masterVolume;
            musicLoopPlayer.volume = musicVolume * masterVolume;
            
            musicStartPlayer.clip = musicStart;
            musicLoopPlayer.clip = musicLoop;

            musicStartPlayer.Play();
            musicLoopPlayer.PlayDelayed(musicStart.length);

            musicPlaying = true;
        }
    }

    public void StartMenuMusic()
    {
        if (musicPlaying)
            StartCoroutine("FadeToMenuMusic");
        else
        {
            StopAllCoroutines();

            musicStartPlayer.volume = musicVolume * masterVolume;
            musicLoopPlayer.volume = musicVolume * masterVolume;

            musicLoopPlayer.clip = menuLoop;

            musicLoopPlayer.Play();

            musicPlaying = true;
        }
    }
}
