using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerClip : MonoBehaviour
{
    public bool playInstantly = false;
    public AudioMixerGroup group;
    public AudioClip audioClip;
    public AudioSourceSettings audioSourceSettings;

    public void Start()
    {
        if (!playInstantly)
        {
            return;
        }

        Play();
    }

    public void Play()
    {
        if (audioClip != null)
        {
            AudioManager.Instance.Play(audioClip, audioSourceSettings, group);
        }
    }

    public void Stop()
    {
        AudioManager.Instance.Stop(group);
    }
}
