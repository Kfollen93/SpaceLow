using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 5.0f;
    private float turnSpeed = 180.0f;
    private float gravity = 30.0f;
    private float yVelocity = 0f;

    private CharacterController controller;
    private Transform player;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        yVelocity -= gravity * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        controller.Move(transform.forward * speed * Time.deltaTime + yVelocity * Vector3.up * Time.deltaTime);
    }
}
