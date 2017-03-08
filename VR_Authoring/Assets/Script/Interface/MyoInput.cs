using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class MyoInput: GestureInput {
    public MyoGesture myo;
    public bool isReady = false;

    public MyoInput() { isReady = false; }
    public MyoInput(MyoGesture _myo) { SetMyo(_myo); }
    public void SetMyo(MyoGesture _myo) { myo = _myo; isReady = true; }
    public override bool Next() { if (isReady) return myo.currentGesture == MyoGesture.Gesture.CLICK; else return false; }
    public override bool Hold() { if (isReady) return myo.currentGesture == MyoGesture.Gesture.FIST_HOLD; else return false; }
    public override bool Pause() { if(isReady) return myo.currentGesture == MyoGesture.Gesture.SPREAD_HOLD; else return false; }
    public override bool Cancel() { if (isReady) return myo.currentGesture == MyoGesture.Gesture.DOUBLE_TAP; else return false; }
}
