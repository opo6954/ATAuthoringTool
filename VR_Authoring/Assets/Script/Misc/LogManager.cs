using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


using UnityEditor;

public class LogManager : MonoBehaviour{

    /*
 * Logging할 내용
 * 
 * 왼쪽/오른쪽 마우스 좌/우 클릭 --> 4가지
 * 드래그 앤 드랍 횟수/시간
 * 최종 걸린 시간(from start to finish까지)
 * 
 * 
 * */

    public static float startTime;

    static string playerName="";

    static string logInfo = "";

    static string logFilePath = "VRExperiment";

    
    
    public enum logType
    {
        Gaze, Tap, FistHold, SpreadHold, DoubleTap     
    }
    
    static void Start()
    {
        Debug.Log("Start");
        if(Directory.Exists(logFilePath) == false)
            Directory.CreateDirectory(logFilePath);
        logInfo = "";
        

        startTime = Time.realtimeSinceStartup;
    }
    
    static public void SaveFile()
    {
        for (int i = 0; i < 20; i++)
            playerName += (int)Random.Range(0, 9);
        Debug.Log("FileSave!");
        saveLog();
    }

    static public void LogCollector(logType lt, float time, float duration, int posIdx)
    {
        string region = "";

        if (posIdx == 1)
        {
            region = " Editing";
        }
        else if(posIdx == 2)
        {
            region = " Training";
        }
        else
            region = " None";

        string completeStr = time + "," + lt.ToString() + "," + duration.ToString() +","+ region;

        //Debug.Log(completeStr);

        logAdder(completeStr);
        
    }

    static public void logAdder(string context)
    {
        logInfo = logInfo + "\r\n" + context;
    }

    static public void LogCollector(logType lt, float time, float duration, string keyName, int posIdx)
    {
        string region = "";

        if (posIdx == 0)
        {
            region = " EDITING";
        }
        else
            region = " TRAINING";

        string completeStr = time + "," + lt.ToString() + "_" + keyName + "," + duration.ToString()+"," + region;

        Debug.Log(completeStr);

        logAdder(completeStr);
    }

    static public void saveLog()
    {
        
        StreamWriter sw = new StreamWriter(logFilePath + "/" + playerName + ".csv",true,System.Text.Encoding.GetEncoding("utf-8"));
        sw.Write(logInfo);
        sw.Close();
        Debug.Log("File Write Done~!");
        //using (Stream filestream = File.Open("VRLogging/" + playerName + ".txt", FileMode.Create))
    }
}
