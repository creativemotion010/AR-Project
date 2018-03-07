using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class RealEstatePlayer : MonoBehaviour
{
    public new Transform camera;
    public float rotationSpeed = 5;
    public float upAxis = 5;
    public float gravity = 0.1f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float horizontal, vertical;
    private float mouseX;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        vertical = CrossPlatformInputManager.GetAxis("Vertical");
        mouseX = CrossPlatformInputManager.GetAxis("Mouse X");

        if (!controller.isGrounded)
            upAxis -= gravity;

        moveDirection = new Vector3(horizontal, upAxis, vertical);



        controller.Move(transform.TransformDirection(moveDirection) * Time.deltaTime);

        transform.Rotate(new Vector3(0, mouseX * rotationSpeed, 0));
        camera.Rotate(0, 0, vertical);
    }
}