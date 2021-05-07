using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // Standard enemy movement
    private float speed = 5.0f;
    private float turnSpeed = 180.0f;
    private float gravity = 30.0f;
    private float yVelocity = 0f;
    private CharacterController controller;
    private Transform player;

    // Shooting portion of script.
    private float timeBtwShots;
    private float startTimeBtwShots;
    public GameObject projectile;

    private void Start()
    {
        // enemy movement
        controller = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player").transform;

        // enemy shooting
        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        // enemy movement
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        yVelocity -= gravity * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

        controller.Move(transform.forward * speed * Time.deltaTime + yVelocity * Vector3.up * Time.deltaTime);

        // enemy shooting
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}