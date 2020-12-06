using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public abstract class Debug
{
    private static string RetraceCaller(string file, string func, int line) {

        string[] fileName = file.Split('\\');

        return $"[{fileName[fileName.Length-1]}:{line}]";
     }

    public static void Log(object data, [CallerFilePath]string filePathName = null, [CallerMemberName]string functionName = null, [CallerLineNumber]int lineNumber = 0)
    {
        LogData('I',RetraceCaller(filePathName, functionName, lineNumber), data);
    }

    public static void LogError(object data, [CallerFilePath]string filePathName = null, [CallerMemberName]string functionName = null, [CallerLineNumber]int lineNumber = 0)
    {
        LogData('E',RetraceCaller(filePathName, functionName, lineNumber), data);
    }

    public static void LogWarn(object data, [CallerFilePath]string filePathName = null, [CallerMemberName]string functionName = null, [CallerLineNumber]int lineNumber = 0)
    {
        LogData('W',RetraceCaller(filePathName, functionName, lineNumber), data);
    }


    static void LogData(char flag ,string calller ,object data)
    {
#if UNITY_EDITOR //only used for the editor.....
        switch (flag)
        {
            case 'I':
                UnityEngine.Debug.LogFormat("{0}:=> {1}", calller, data);
                break;
            case 'E':
                UnityEngine.Debug.LogErrorFormat("{0}:=> {1}", calller, data);
                break;
            case 'W':
                UnityEngine.Debug.LogWarningFormat("{0}:=> {1}", calller, data);
                break;
        }
#endif

    }
}
