using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public enum Streams
{
    Music,
    Effect
};

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    private readonly Dictionary<AudioMixerGroup, AudioSource> groupSourceDictionary = new();

    // Singleton instance
    public static AudioManager Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GenerateAudioSources();
    }

    private void GenerateAudioSources()
    {
        AudioMixerGroup[] audioMixerGroups = audioMixer.FindMatchingGroups("Master/");
        foreach (AudioMixerGroup audioGroup in audioMixerGroups)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;
            audioSource.outputAudioMixerGroup = audioGroup;

            groupSourceDictionary.Add(audioGroup, audioSource);
        }
    }

    public void Play(AudioClip clip, AudioSourceSettings audioSourceSettings, AudioMixerGroup stream)
    {
        AudioSource source = groupSourceDictionary[stream];

        source.clip = clip;
        source.volume = audioSourceSettings.volume;
        source.loop = audioSourceSettings.loop;
        source.pitch = Random.Range(audioSourceSettings.lowestPitchRange, audioSourceSettings.highestPitchRange);
        source.Play();
    }

    public void Stop(AudioMixerGroup group)
    {
        AudioSource source = groupSourceDictionary[group];

        source.Stop();
    }

    //Example usage: SetVolume("musicVol", -80f);
    public void SetVolume(string parameter, float value)
    {
        _ = audioMixer.SetFloat(parameter, value);
    }
}
