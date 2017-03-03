using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class MyoInput: GestureInput {
    public MyoGesture myo;

    public MyoInput() { }
    public MyoInput(MyoGesture _myo) { myo = _myo; }
    public override bool Next() { return myo.currentGesture == MyoGesture.Gesture.CLICK; }
    public override bool Hold() { return myo.currentGesture == MyoGesture.Gesture.FIST_HOLD; }
    public override bool Pause() { return myo.currentGesture == MyoGesture.Gesture.SPREAD_HOLD; }
    public override bool Cancel() { return myo.currentGesture == MyoGesture.Gesture.DOUBLE_TAP; }
}
