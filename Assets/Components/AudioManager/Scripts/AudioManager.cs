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
	private Dictionary<AudioMixerGroup, AudioSource> groupSourceDictionary = new Dictionary<AudioMixerGroup, AudioSource>();

	// Singleton instance
	public static AudioManager Instance = null;

	private void Awake()
	{
		if (Instance != null && Instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
		
		Instance = this;
		DontDestroyOnLoad(gameObject);
    }

	private void Start() {
		GenerateAudioSources();
	}

	private void GenerateAudioSources() {
        var audioMixerGroups = audioMixer.FindMatchingGroups ("Master/");
		foreach(AudioMixerGroup audioGroup in audioMixerGroups)
		{
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.volume = 0.5f;
			audioSource.outputAudioMixerGroup = audioGroup;

			groupSourceDictionary.Add(audioGroup, audioSource);
		}
	}

	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip, AudioSourceSettings audioSourceSettings, AudioMixerGroup stream)
	{
		var source = groupSourceDictionary[stream];

		source.clip = clip;
		source.volume = audioSourceSettings.volume;
		source.loop = audioSourceSettings.loop;
		source.pitch = Random.Range(audioSourceSettings.lowestPitchRange, audioSourceSettings.highestPitchRange);
		source.Play();
	}

	public void Stop(AudioMixerGroup group)
	{
		var source = groupSourceDictionary[group];

		source.Stop();
	}

	//SetVolume("musicVol", -80f);
	public void SetVolume(string parameter, float value) {
        audioMixer.SetFloat(parameter, value);
    }

	// // Play a random clip from an array, and randomize the pitch slightly.
	// public void RandomSoundEffect(AudioMixerGroup group, params AudioClip[] clips)
	// {
	// 	var source = groupSourceDictionary[group];

	// 	int randomIndex = Random.Range(0, clips.Length);
	// 	float randomPitch = Random.Range(group.lowestPitchRange, group.highestPitchRange);

	// 	source.pitch = randomPitch;
	// 	source.clip = clips[randomIndex];
	// 	source.Play();
	// }
}
