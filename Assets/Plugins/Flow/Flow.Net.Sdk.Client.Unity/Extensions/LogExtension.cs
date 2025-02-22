using UnityEngine;

namespace Flow.Net.SDK.Extensions
{
    public static class LogExtension
    {
        public static bool EnableDebugMode { get; set; }

        static LogExtension()
        {
            EnableDebugMode = true;
        }
        
        public static void ToLog(this string logContent)
        {
            logContent = $"---------- DEBUG - {logContent}";
            if(EnableDebugMode)
            {
                Debug.Log(logContent);
            }
        }
    }
}