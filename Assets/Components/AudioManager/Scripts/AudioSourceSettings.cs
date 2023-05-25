using UnityEngine;
using System;

[Serializable]
public class AudioSourceSettings
{
    [Range(0, 1)] public float volume = 0.5f;
    public bool loop = false;
    [Range(-3f, 3f)] public float lowestPitchRange = 1.0f;
    [Range(-3f, 3f)] public float highestPitchRange = 1.0f;
}
