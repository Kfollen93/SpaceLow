using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTurretSound : MonoBehaviour
{
    public AudioSource secondAudioSrc;

    private void Start()
    {
        secondAudioSrc = GetComponent<AudioSource>();
    }
  
    public void TurretReloadSound()
    {
        secondAudioSrc.Play();
    }
}
