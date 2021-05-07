using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    public AudioSource thirdAudioSrc;

    private void Start()
    {
        thirdAudioSrc = GetComponent<AudioSource>();
    }

    public void TakeDmgSoundEffect()
    {
        AudioClip clip = GetRandomClip();
        thirdAudioSrc.volume = Random.Range(0.5f, 0.6f);
        thirdAudioSrc.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}
