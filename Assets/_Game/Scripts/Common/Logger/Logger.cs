using System.Diagnostics;
using UnityEngine;

public static class Logger
{
    [Conditional("DEV_VER")]
    public static void Log(string msg)
    {
        UnityEngine.Debug.LogFormat("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
    
    [Conditional("DEV_VER")]
    public static void LogWarning(string msg)
    {
        UnityEngine.Debug.LogWarningFormat("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
    
    [Conditional("DEV_VER")]
    public static void LogWithColor(string msg, Color color)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(color);
        
        UnityEngine.Debug.LogFormat(
            "[{0}] <color=#{1}>{2}</color>", 
            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), 
            hexColor, 
            msg
        );
    }
    
    public static void LogError(string msg)
    {
        UnityEngine.Debug.LogErrorFormat("[{0}] {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
}
