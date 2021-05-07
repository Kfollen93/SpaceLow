using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    CharacterController controller;
    AudioSource audioSrc;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Step();
    }

    // The proper way to do this is have this function called as an animation event for each time the character's foot touches the ground.
    // This requires you set it for each animation; forward, back, etc. but it's accurate then.
    private void Step()
    {
        if (controller.isGrounded && controller.velocity.magnitude >2f && !audioSrc.isPlaying)
        {
            audioSrc.volume = Random.Range(0.20f, 0.25f);
            audioSrc.pitch = Random.Range(0.7f, 1.1f);
            audioSrc.Play();
        }
    }
}
