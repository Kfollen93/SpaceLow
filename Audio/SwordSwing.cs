using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;
    public AudioSource secondAudioSrc;

    private void Start()
    {
        secondAudioSrc = GetComponent<AudioSource>();
    }

    // This function is being called using the Animation Events.  
    private void SwordSwingSound()
    {
        AudioClip clip = GetRandomClip();
        secondAudioSrc.volume = Random.Range(0.2f, 0.3f);
        secondAudioSrc.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

}
