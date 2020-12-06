interface ILogger
{
    void Log(object data);
    void Log(string format, params string[] data);

    void LogWarn(object data);
    void LogWarn(string format, params string[] data);

    void LogError(object data);
    void LogError(string format, params string[] data);

}
