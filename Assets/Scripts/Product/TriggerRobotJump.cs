using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRobotJump : MonoBehaviour
{
    public GameObject robot1;
    public GameObject robot2;
    public Animator robotAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Spaceship"))
        {
            robotAnimator.SetTrigger("Jump");
        }
    }
}
