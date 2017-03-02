using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureInput {
    public enum GestureType
    {
        MOUSE_L_DOWN =0,
        MOUSE_L_PRESS = 1,
        MOUSE_L_UP = 2,
        MOUSE_R_DOWN = 3,
        MOUSE_R_PRESS = 4,
        MOUSE_R_UP = 5,
        MYO_FIST= 6,
        MYO_SPREAD = 7,
        MYO_WAVE_IN = 8,
        MYO_WAVE_OUT = 9,
        MYO_REST = 10,
        MYO_D_TAP = 11,
        KEY_SPACE = 12
    }

    public int InputID;

    public virtual bool Next() { return false;  }
    public virtual bool Hold() { return false; }
    public virtual bool Pause() { return false; }
    public virtual bool Cancel() { return false; }
}
