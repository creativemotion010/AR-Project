using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour {
    public GameObject Char2;
    public GameObject Char3;

	public void WalkAway()
    {
        Char2.GetComponent<Animator>().SetTrigger("WalkAway");
    }

    public void Fight()
    {
        Char3.GetComponent<Animator>().SetTrigger("Fight");
    }
}
