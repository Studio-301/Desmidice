using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public static SoundBank Instance;

    public SerializedDictionary<string, List<AudioClip>> Clips;

    public AudioSource Source;

    void Awake()
    {
        Instance = this;
    }

    public void PlayClip(string id)
    {
        Source.PlayOneShot(Clips[id][Random.Range(0, Clips.Count)]);
    }

    public void PlayClip(string id, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(Clips[id][Random.Range(0, Clips.Count)], position);
    }
}
