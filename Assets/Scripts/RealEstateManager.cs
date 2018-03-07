using System;
using UnityEngine;

public class RealEstateManager : MonoBehaviour
{
    public GameObject[] markers;
    public Transform centerOfAttention;
    public GameObject Markers;
    private GameObject activeModel;
    private GameObject activeMarker;
    private Vector3 modelInitialScale;
    private Vector3 modelInitialPosition;
    private Transform markersInitialTransform;

    public GameObject ActiveMarker
    {
        set
        {
            if (activeMarker != value)
            {
                activeMarker = value;
                OnMarkerClicked();
            }
        }
        get
        {
            return activeMarker;
        }
    }

    public static RealEstateManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _InputController.Instance.OnDoubleClick += OnDoubleClick;
    }

    private void OnDoubleClick(bool twice)
    {
        if (!twice)
        {
            activeModel.transform.localPosition = centerOfAttention.localPosition;
            activeModel.transform.localScale = centerOfAttention.localScale;
        }
        else
        {
            activeModel.transform.localPosition = modelInitialPosition;
            activeModel.transform.localScale = modelInitialScale;
            ReturnToAllMarkers();
        }
    }

    private void ReturnToAllMarkers()
    {
        activeModel.SetActive(false);
        ActiveMarker = null;
        for (int i = 0; i < markers.Length; i++)
        {
            markers[i].GetComponent<Animator>().enabled = true;
        }
    }

    private void OnMarkerClicked()
    {
        if (ActiveMarker != null)
        {
            //activeModel = ActiveMarker.GetComponent<RealEstateMarker>().Model;
            activeModel.SetActive(true);
            modelInitialPosition = activeModel.transform.localPosition;
            modelInitialScale = activeModel.transform.localScale;
            // hide the rest of markers model
            for (int i = 0; i < markers.Length; i++)
            {
                if (markers[i] != ActiveMarker)
                {
                    //markers[i].GetComponent<RealEstateMarker>().Model.SetActive(false);
                }
                markers[i].GetComponent<Animator>().enabled = false;
            }
        }
    }

    void OnEnable()
    {
        markersInitialTransform = Markers.transform;
    }
    void OnDisable()
    {
        Markers.SetActive(false);
        Markers.transform.localScale = markersInitialTransform.localScale;
        Debug.Log("on diable");
    }
}
