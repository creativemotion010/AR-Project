using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform targetToShoot;
    public LayerMask CharacterLayer;
    public bool targetOnFire;
    public float radius = 0.5f;
    public float timer;

    private void Update()
    {
        if (targetOnFire)
        {
            transform.LookAt(targetToShoot.position);
            transform.position = Vector3.MoveTowards(transform.position, targetToShoot.position, 0.02f);

            timer += Time.deltaTime;

            ShootTarget();
        }
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, CharacterLayer);

        if (colliders.Length != 0)
        {
            targetOnFire = true;
            targetToShoot = colliders[0].transform;
            print("found target" + targetToShoot.name);
        }
    }

    private void ShootTarget()
    {
        if (timer >= 0.5f)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform);
            bullet.transform.LookAt(targetToShoot);
            bullet.GetComponent<Rigidbody>().velocity = targetToShoot.position - transform.position + new Vector3(0, 0.5f, 0);
            timer = 0;
        }
        targetToShoot = null;
        targetOnFire = false;
    }
}