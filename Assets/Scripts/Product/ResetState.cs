using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetState : MonoBehaviour {

    private void OnEnable()
    {
        if (Bottle.Instance != null)
        {
            if (Bottle.Instance.SqueezedFruits.Count > 0)
            {
                Reset();
            }
        }

    }

    private void Reset()
    {
        for (int i = 0; i < Bottle.Instance.SqueezedFruits.Count; i++)
        {
            //Bottle.Instance.SqueezedFruits[i].transform.position = 
            //    Bottle.Instance.SqueezedFruits[i].InitialPosition;
            //Bottle.Instance.SqueezedFruits[i].gameObject.SetActive(true);
        }
        Bottle.Instance.SqueezedFruits.Clear();
    }
}
