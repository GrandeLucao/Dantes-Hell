using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(-1f, 1f)]
    public float panStereo;
    [Range(0f, 1f)]
    public float spatialBlend;




    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
