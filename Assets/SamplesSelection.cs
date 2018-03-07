using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplesSelection : MonoBehaviour {

   public int Sample_num;
	// Use this for initialization
	void Start () {
		
	}

    void OnMouseDown()
    {
        VillaSamplesManager.Instance.SelectCurrentSample(Sample_num);

    }
}
