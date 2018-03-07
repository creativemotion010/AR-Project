using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _InputController : MonoBehaviour
{
    public Action<bool> OnDoubleClick;
    private Ray ray;
    private Camera mainCamera;
    private RaycastHit hit;
    private float _sensitivity = 2f;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool active = false;
    private bool one_click = false;
    private float timer_for_double_click;
    private float delay = 2f;
    public static _InputController Instance;
    private int double_click_count = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // for rotation
        if (Input.GetMouseButton(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.transform.tag.Equals("Interactable"))
                {
                    _mouseOffset = (Input.mousePosition - _mouseReference);
                    _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
                    hit.collider.gameObject.transform.Rotate(_rotation);
                    _mouseReference = Input.mousePosition;
                }
                else if (hit.collider.gameObject.transform.tag.Equals("Wolf"))
                {
                    active = !active;
                    EnableAnimator();
                }

            }
        }

        // for double clicking: zooming in/out
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.transform.tag.Equals("Interactable"))
                {
                    if (!one_click)
                    {
                        one_click = true;
                        timer_for_double_click = Time.time;
                    }
                    else
                    {
                        if (Time.time - timer_for_double_click > delay)
                        {
                            one_click = false;
                        }
                        else
                        {
                            one_click = false;
                            double_click_count++;
                            if (OnDoubleClick != null)
                            {
                                if (double_click_count == 1)
                                {
                                    OnDoubleClick.Invoke(false);
                                }
                                else
                                {
                                    OnDoubleClick.Invoke(true);
                                    double_click_count = 0;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void EnableAnimator()
    {
        hit.collider.gameObject.GetComponent<Animator>().enabled = active;
    }
}

