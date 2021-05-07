using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayAnimations();
    }

    private void PlayAnimations()
    {
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isStrafeL", true);
        }
        else
        {
            anim.SetBool("isStrafeL", false);
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isStrafeR", true);
        }
        else
        {
            anim.SetBool("isStrafeR", false);
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("isWalkingBackW", true);
        }
        else
        {
            anim.SetBool("isWalkingBackW", false);
        }
    }
}
