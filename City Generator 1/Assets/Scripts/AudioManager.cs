using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private static AudioSource audioSource;

    public static AudioClip GetAudio(string name)
    {
        return audioClips[name];
    }

    public static void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Object[] _audio = Resources.LoadAll("Audio", typeof(AudioClip));

        foreach (Object temp in _audio)
        {
            audioClips.Add(temp.name, (AudioClip)temp);
        }
    }
}
