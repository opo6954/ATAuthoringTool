using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class InteractableObject : TapToPlace
{
    public float approachingThreshold = 2.0f;

    // Use this for initialization
    void Start () {
        SavedAnchorFriendlyName = gameObject.transform.name;
	}

    public void Update()
    {

    }

    public float GetCameraDistance()
    {
        return Vector3.Distance(this.transform.position, Camera.main.transform.position);
    }
}
