﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thalmic.Myo;

public class MyoGesture:MonoBehaviour{
    public enum Gesture { NONE, FIST_HOLD, CLICK, SPREAD_HOLD, DOUBLE_TAP }

    public ThalmicMyo myo;
    public Gesture currentGesture = Gesture.NONE;
    public int streamLength = 5;
    public int blockTerm = 10;
    public bool isLeft = false;
    public bool isRight = false;
    public LogManager logManager;
    public ModeSelector modeSelector;

    int blockCount = 0;
    bool isBlocked = false;
    int fistCount = 0;
    int spreadCount = 0;
    List<Thalmic.Myo.Pose> stream;
    
    public void Start()
    {
        stream = new List<Thalmic.Myo.Pose>();
    }

    void UpdateStream()
    {
        if (myo.arm == Arm.Left)
        {
            isLeft = true;
            isRight = false;
        }
        if (myo.arm == Arm.Right)
        {
            isRight = true;
            isLeft = false;
        }

        if (isBlocked)
            return;
        stream.Add(myo.pose);
        if (stream.Count > streamLength)
            stream.RemoveAt(0);
    }

    void Flush()
    {
        spreadCount = 0;
        fistCount = 0;
        stream.Clear();
        StartBlocking();
    }

    void StartBlocking()
    {
        blockCount = blockTerm;
        isBlocked = true;
    }

    void UpdateBlocking()
    {
        if(isBlocked)
            blockCount--;
        if (blockCount < 0)
            isBlocked = false;
    }

    void JudgeGesture()
    {
        currentGesture = Gesture.NONE;
        UpdateBlocking();

        if (isBlocked)
            return;

        // when the length of stream is not enough, just quit
        if (stream.Count < streamLength)
        {
            return;
        }
        
        // check the stream
        for(int i=0; i<streamLength; i++)
        {
            if(stream[i] == Pose.DoubleTap)
            {
                currentGesture = Gesture.DOUBLE_TAP;
                Flush();
                LogManager.logType lt = LogManager.logType.DoubleTap;
                LogManager.LogCollector(lt, Time.realtimeSinceStartup - LogManager.startTime, 0, (int)modeSelector.currentMode);
                Debug.Log("MYO GESTURE : Double tap");
                return;
            }
            else if(stream[i] == Pose.FingersSpread)
            {
                spreadCount++;
                fistCount = 0;

            }
            else if(stream[i] == Pose.Fist)
            {
                fistCount++;
                spreadCount = 0;
            }
        }

        if (spreadCount >= streamLength)
        {
            currentGesture = Gesture.SPREAD_HOLD;
            Flush();
            LogManager.logType lt = LogManager.logType.SpreadHold;
            LogManager.LogCollector(lt, Time.realtimeSinceStartup - LogManager.startTime, 0, (int)modeSelector.currentMode);
            Debug.Log("MYO GESTURE : Finger spread hold");
        }
        if (fistCount >= streamLength)
        {
            LogManager.logType lt = LogManager.logType.FistHold;
            LogManager.LogCollector(lt, Time.realtimeSinceStartup - LogManager.startTime, 0, (int)modeSelector.currentMode);
            currentGesture = Gesture.FIST_HOLD;
            Flush();
            //Debug.Log("MYO GESTURE : Fist hold");
        }
    }

    public void Update()
    {
        UpdateStream();
        JudgeGesture();
    }

    public virtual string ToString()
    {
        return myo.gyroscope.ToString();
    }
}
