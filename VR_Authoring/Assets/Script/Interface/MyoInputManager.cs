using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class MyoInputManager:MonoBehaviour{
    public MyoInput myoInputLeft;
    public MyoInput myoInputRight;
    public MyoGesture myoGes1;
    public MyoGesture myoGes2;

    public void Start()
    {
        myoInputLeft = new MyoInput();
        myoInputRight = new MyoInput();
    }

    public void Update()
    {
        if (!myoInputLeft.isReady)
        {
            if (myoGes1.isLeft)
            {
                myoInputLeft = new MyoInput(myoGes1);
                Debug.Log("Left myo is ready to deploy!");
            }
                
            if (myoGes2.isLeft)
            {
                myoInputLeft = new MyoInput(myoGes2);
                Debug.Log("Left myo is ready to deploy!");
            }
                
        }
        if (!myoInputRight.isReady)
        {
            if (myoGes1.isRight)
            {
                myoInputRight = new MyoInput(myoGes1);
                Debug.Log("Right myo is ready to deploy!");
            }
                
            if (myoGes2.isRight)
            {
                myoInputRight = new MyoInput(myoGes2);
                Debug.Log("Right myo is ready to deploy!");
            }                
        }
            
    }
}
