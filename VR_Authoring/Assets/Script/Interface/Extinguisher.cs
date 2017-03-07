using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class Extinguisher: MonoBehaviour {
    public MyoInputManager myoManager;
    public GameObject smokeEffect;
    public GameObject smokeSound;
    public GameObject hoseModel;
    public bool isReady = false;

    MyoGesture gestureLeft;
    MyoGesture gestureRight;

    UnityEngine.Vector3 inactivatedPosition;
    float step;
    float offset = 0.05f;
    public void Start()
    {
        inactivatedPosition = new UnityEngine.Vector3(0, -0.1f, 0);
        step = 1.0f;
        StopShooting();
        Relax();
    }

    public void SetHoseActive()
    {
        hoseModel.SetActive(true);
    }

    public void SetHoseDeactive()
    {
        hoseModel.SetActive(false);
    }

    void GetReady()
    {
        // adjustment of myo rotation
        UnityEngine.Quaternion myoQuat = UnityEngine.Quaternion.Euler(myoManager.myoInputLeft.myo.myo.gyroscope);
        UnityEngine.Quaternion relative = UnityEngine.Quaternion.Inverse(this.transform.rotation) * myoQuat;
        this.transform.localRotation = UnityEngine.Quaternion.Slerp(UnityEngine.Quaternion.identity, UnityEngine.Quaternion.Inverse(myoQuat), 0.2f);

        // smooth translation
        this.transform.localPosition = UnityEngine.Vector3.Lerp(UnityEngine.Vector3.zero, inactivatedPosition, step);
        if (step > 0.0f)
        {
            step -= offset;
        }
        isReady = true;
    }

    void Shoot()
    {
        if (!isReady)
            return;
        smokeEffect.GetComponent<ParticleSystem>().Play();
        if (!smokeSound.active)
            smokeSound.SetActive(true);
    }

    void StopShooting()
    {
        smokeEffect.GetComponent<ParticleSystem>().Stop();
        if (smokeSound.active)
            smokeSound.SetActive(false);
    }

    void Relax()
    {
        this.transform.localPosition = UnityEngine.Vector3.Lerp(UnityEngine.Vector3.zero, inactivatedPosition, step);
        if (step < 1.0f)
        {
            step += offset;
        }
        this.transform.localRotation = UnityEngine.Quaternion.Slerp(this.transform.localRotation, UnityEngine.Quaternion.identity, 0.2f);
        isReady = false;        
    }

    private void Update()
    {
        if (!hoseModel.active)
            return;
        // for making the hose ready (left hand)
        if (myoManager.myoInputLeft.isReady && myoManager.myoInputRight.isReady)
        {
            if (myoManager.myoInputLeft.isReady)
            {
                if (myoManager.myoInputLeft.myo.myo.pose == Thalmic.Myo.Pose.Fist)
                {
                    GetReady();
                }
                else
                {
                    Relax();
                }
            }
            else
            {
                Relax();
            }

            // for shooting (right hand)
            if (myoManager.myoInputRight.isReady && isReady)
            {
                if (myoManager.myoInputRight.myo.myo.pose == Thalmic.Myo.Pose.Fist)
                {
                    Shoot();
                }
                else
                {
                    StopShooting();
                }
            }
            else
                StopShooting();
        }
            
    }
}
