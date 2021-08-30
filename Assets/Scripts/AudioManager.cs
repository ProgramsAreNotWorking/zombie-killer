using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] string[] clipNames;
    [SerializeField] AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playAudio(string anim, float pitch = 0.8f)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (clipNames[i] == anim)
            {
                source.clip = clips[i];
                source.pitch = pitch;
                source.Play();
                break;
            }
        }
    }
}
