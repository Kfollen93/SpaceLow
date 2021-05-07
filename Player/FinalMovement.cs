using System.Collections;
using UnityEngine;

public class FinalMovement : MonoBehaviour
{
    private float speed = 10f;
    private float sprint = 0.4f;
    private float rotationSpeed = 2f;
    private float yVelocity = 0f;
    private float jumpSpeed = 15f;
    private float gravity = 6000f;
    private bool PlayerIsMoving => controller.velocity.sqrMagnitude > 0.1f;
    private bool PlayerHasStamina => StaminaBar.Instance.currentStamina > 0.1f;
    private bool PlayerCanSprint => PlayerIsMoving && PlayerHasStamina;
    private Vector3 sprintMovement;
    private Vector3 baseMovement;
    private Vector3 input;
    private CharacterController controller;
    private Animator anim;

    // Controls turning the camera with the mouse
    private float currentCameraHeadRotation = 0, maxCameraHeadRotation = 80.0f, minCameraHeadRotation = -80.0f;
    public Transform followTarget;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerCameraMouseStrafe();
        Jump();
        Movement();
    }

    private void Movement()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        yVelocity -= gravity * Time.deltaTime;
        baseMovement = transform.TransformDirection(input.normalized * speed * Time.deltaTime + yVelocity * Vector3.up * Time.deltaTime);
        controller.Move(baseMovement);
        Sprint();
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && PlayerCanSprint)
        {
            sprintMovement = transform.TransformDirection(input.normalized * speed * sprint * Time.deltaTime + yVelocity * Vector3.up * Time.deltaTime);
            controller.Move(sprintMovement);
            StaminaBar.Instance.UseStamina(0.05f);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !PlayerCanSprint)
        {
            return; // No stamina
        }
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            gravity = 6000f; // High gravity to prevent isGrounded from becoming false when on slopes.
            yVelocity = -controller.stepOffset / Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                gravity = 55f; // When intending to jump, change the gravity back to the original amount that felt right (55f) and jump.
                yVelocity = jumpSpeed;
                anim.SetBool("isJumping", true);
            }
            else
            {
                // Reset vertical velocity, could be 0f, but I found 0.01f ensured the yVelocity was being reset.
                yVelocity = 0.01f;
            }
        }
        else
        {
            gravity = 55f; // Resets gravity anytime the player is not grounded.
            anim.SetBool("isJumping", false);
        }
    }

    private void PlayerCameraMouseStrafe()
    {
        // Control left and right turning of camera/player with mouse
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(Vector3.up, mouseInput.x * rotationSpeed);

        // Control up and down turning of camera with mouse
        currentCameraHeadRotation = Mathf.Clamp(currentCameraHeadRotation + mouseInput.y * rotationSpeed, minCameraHeadRotation, maxCameraHeadRotation);
        followTarget.localRotation = Quaternion.identity;
        followTarget.Rotate(Vector3.left, currentCameraHeadRotation);
    }
}
