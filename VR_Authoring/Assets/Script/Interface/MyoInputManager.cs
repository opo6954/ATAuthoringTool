using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class MyoInputManager:MonoBehaviour{
    public MyoInput myoInputLeft;
    public MyoInput myoInputRight;
    public MyoGesture myoGes1;
    public MyoGesture myoGes2;
    public int numOfMyos;

    public void Vibrate()
    {
        if(myoInputLeft.isReady)
            myoInputLeft.myo.myo.Vibrate(VibrationType.Short);
        if (myoInputRight.isReady)
            myoInputRight.myo.myo.Vibrate(VibrationType.Short);
    }
    public void Start()
    {
        numOfMyos = 0;
        myoInputLeft = new MyoInput();
        myoInputRight = new MyoInput();
    }

    public void Update()
    {
        if (myoGes1.isLeft)
        {
            myoInputLeft.SetMyo(myoGes1);
            if (myoInputRight.isReady)
                numOfMyos = 2;
            else
                numOfMyos = 1;
            //Debug.Log("Left myo is ready to deploy!");
        }
                
        if (myoGes2.isLeft)
        {
            myoInputLeft.SetMyo(myoGes2);
            if (myoInputRight.isReady)
                numOfMyos = 2;
            else
                numOfMyos = 1;
            //Debug.Log("Left myo is ready to deploy!");
        }
                
        
        if (myoGes1.isRight)
        {
            myoInputRight.SetMyo(myoGes1);
            if (myoInputLeft.isReady)
                numOfMyos = 2;
            else
                numOfMyos = 1;
            //Debug.Log("Right myo is ready to deploy!");
        }
                
        if (myoGes2.isRight)
        {
            myoInputRight.SetMyo(myoGes2);
            if (myoInputLeft.isReady)
                numOfMyos = 2;
            else
                numOfMyos = 1;
            //Debug.Log("Right myo is ready to deploy!");
        }                
        
            
    }
}
