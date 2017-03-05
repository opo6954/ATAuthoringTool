using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class Extinguisher: MonoBehaviour {
    public MyoGesture myo;

    public void Start()
    {

    }

    private void Update()
    {
        if(myo.myo.pose == Thalmic.Myo.Pose.Fist)
            this.transform.eulerAngles = myo.myo.gyroscope;
    }
}
