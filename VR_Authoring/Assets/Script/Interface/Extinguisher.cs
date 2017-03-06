using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class Extinguisher: MonoBehaviour {
    public MyoGesture myo;
    public GameObject smokeEffect;
    public GameObject smokeSound;

    UnityEngine.Vector3 inactivatedPosition;
    float step;
    float offset = 0.05f;
    public void Start()
    {
        inactivatedPosition = new UnityEngine.Vector3(0, -0.1f, 0);
        step = 1.0f;
    }

    void Activate()
    {
        // adjustment of myo rotation
        UnityEngine.Quaternion myoQuat = UnityEngine.Quaternion.Euler(myo.myo.gyroscope);
        UnityEngine.Quaternion relative = UnityEngine.Quaternion.Inverse(this.transform.rotation) * myoQuat;
        this.transform.localRotation = UnityEngine.Quaternion.Slerp(UnityEngine.Quaternion.identity, UnityEngine.Quaternion.Inverse(myoQuat), 0.2f);

        // smooth translation
        this.transform.localPosition = UnityEngine.Vector3.Lerp(UnityEngine.Vector3.zero, inactivatedPosition, step);
        if (step > 0.0f)
        {
            step -= offset;
        }
        if(!smokeEffect.active)
            smokeEffect.SetActive(true);
        if (!smokeSound.active)
            smokeSound.SetActive(true);
    }

    void Deactivate()
    {
        this.transform.localPosition = UnityEngine.Vector3.Lerp(UnityEngine.Vector3.zero, inactivatedPosition, step);
        if (step < 1.0f)
        {
            step += offset;
        }
        this.transform.localRotation = UnityEngine.Quaternion.Slerp(this.transform.localRotation, UnityEngine.Quaternion.identity, 0.2f);
        if (smokeEffect.active)
            smokeEffect.SetActive(false);
        if (smokeSound.active)
            smokeSound.SetActive(false);
    }

    private void Update()
    {
        if(myo.myo.pose == Thalmic.Myo.Pose.Fist)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
}
