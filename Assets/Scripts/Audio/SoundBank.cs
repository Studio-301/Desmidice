using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public static SoundBank Instance;

    public SerializedDictionary<string, List<AudioClip>> Clips;

    public AudioSource Source;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void PlayClip(string id)
    {
        if (Clips.TryGetValue(id, out var variants))
        {
            var sfx = variants[Random.Range(0, variants.Count)];
            Source.PlayOneShot(sfx);
        }
        else
            Debug.LogError($"Unknown ID: {id}");
    }

    public void PlayClip(string id, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(Clips[id][Random.Range(0, Clips.Count)], position);
    }
}
