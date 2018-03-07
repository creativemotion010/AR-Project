using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. create position within circle
/// 2. Move the player to this position not periodically
/// </summary>
public class Wander : MonoBehaviour
{
    public float maxVelocity = 0.005f;
    GameObject[] seek_points = new GameObject[4];
    Rigidbody body;
    Vector3 desired_velocity;
    Vector3 steering_force;
    GameObject current_seek_point;
    Animator animator;
    bool doOnce = false;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GenerateSeekPoints();
    }

    private void Update()
    {
        if (current_seek_point == null || Mathf.FloorToInt(current_seek_point.transform.position.x) ==
            Mathf.FloorToInt(transform.position.x) && Mathf.FloorToInt(current_seek_point.transform.position.z) ==
            Mathf.FloorToInt(transform.position.z))
        {
            body.velocity = Vector3.zero;
            if (!doOnce)
            {
                animator.SetTrigger("Stop");
                Invoke("GetSeekPoint", UnityEngine.Random.Range(4, 20));
                doOnce = true;
            }
            RandomizeMovement();
        }
        else
        {
            Seek();
        }
    }

    private void GenerateSeekPoints()
    {
        Vector2 randomXZ;
        Vector3 new_position;
        GameObject parent = new GameObject(this.name + " Seek Points");
        parent.transform.position = Vector3.zero;
        seek_points[0] = Instantiate(new GameObject("Seek Point 0"), transform.position, Quaternion.identity);
        seek_points[0].transform.parent = parent.transform;
        for (int i = 1; i < seek_points.Length; i++)
        {
            randomXZ = UnityEngine.Random.insideUnitCircle;
            new_position = new Vector3(transform.position.x + randomXZ.x, transform.position.y,
                transform.position.z + randomXZ.y);
            seek_points[i] = Instantiate(new GameObject("Seek Point " + i), new_position, Quaternion.identity);
            seek_points[i].transform.parent = parent.transform;
        }
    }

    private void GetSeekPoint()
    {
        animator.SetTrigger("Move");
        current_seek_point = seek_points[UnityEngine.Random.Range(0, seek_points.Length)];
        doOnce = false;
    }

    private void Seek()
    {
        desired_velocity = (current_seek_point.transform.position - transform.position).normalized * maxVelocity;
        Debug.DrawLine(transform.position, current_seek_point.transform.position, Color.magenta);
        steering_force = desired_velocity - body.velocity;
        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(transform.forward, desired_velocity, 0.009f, 0f));
        if (Mathf.FloorToInt(Vector3.Angle(transform.forward, desired_velocity)) <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.forward * 10 /*current_seek_point.transform.position*/, 0.002f);
        }
        //body.velocity += new Vector3(steering_force.x, 0, steering_force.z);
    }

    private void RandomizeMovement()
    {
        int rand = UnityEngine.Random.Range(0, 2);
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (rand == 0)
            {
                animator.SetTrigger("Eat");
            }
            else if (rand == 1)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.SetTrigger("Stop");
            }
        }

    }
}
