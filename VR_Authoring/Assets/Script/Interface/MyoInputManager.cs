using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class MyoInputManager:MonoBehaviour{
    public MyoInput myoInput1;
    public MyoInput myoInput2;
    public MyoGesture myoGes1;
    public MyoGesture myoGes2;

    public void Start()
    {
        myoInput1 = new MyoInput(myoGes1);
        myoInput2 = new MyoInput(myoGes2);
    }

    public void Update()
    {
        
    }
}
