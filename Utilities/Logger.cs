// System includes.
using System;
using System.Diagnostics;
// Godot includes.
using Godot;

namespace Utilities;

public static class Logger
{
    private enum LogLevel
    {
        Info = 0,
        Warning,
        Error,
        Fatal,
    }
    
    private const int FramesToSkip = 2;
    
    public static void Info(string toLog)
    {
        Log(LogLevel.Info, GetCallingClass(), toLog);
    }

    public static void Info(Variant toLog)
    {
        Log(LogLevel.Info, GetCallingClass(), toLog.AsString());
    }

    public static void Warning(string toLog)
    {
        Log(LogLevel.Warning, GetCallingClass(), toLog);
    }
    
    public static void Warning(Variant toLog)
    {
        Log(LogLevel.Warning, GetCallingClass(), toLog.AsString());
    }
    
    public static void Error(string toLog)
    {
        Log(LogLevel.Error, GetCallingClass(), toLog);
    }
    
    public static void Error(Variant toLog)
    {
        Log(LogLevel.Error, GetCallingClass(), toLog.AsString());
    }
    
    public static void Fatal(string toLog)
    {
        Log(LogLevel.Fatal, GetCallingClass(), toLog);
    }
    
    public static void Fatal(Variant toLog)
    {
        Log(LogLevel.Fatal, GetCallingClass(), toLog.AsString());
    }

    private static string GetCallingClass()
    {
        var declaringType = new StackFrame(FramesToSkip).GetMethod()?.DeclaringType;
        return declaringType == null ? "UNKNOWN" : declaringType.Name;
    }

    private static void Log(LogLevel level, string caller, string toLog)
    {
        GD.Print($"[{level.ToString().ToUpper()}] - {DateTime.Now:hh:mm:ss}: {caller} - {toLog}");
    }
}