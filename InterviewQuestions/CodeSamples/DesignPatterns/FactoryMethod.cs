using System;

namespace FactoryMethod
{

    public enum LoggerType
    {
        Console,
        File
    }


    public interface ILogger
    {
        void Log(string message);
    }

    public interface ILoggerFactory
    {
        ILogger CreateLogger(LoggerType loggerType);
    }

    public abstract class LoggerFactory : ILoggerFactory
    {        
        public abstract ILogger CreateLogger(LoggerType loggerType);
    }

    public class ConsoleLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger(LoggerType loggerType)
        {
            return new ConsoleLogger();
        }
    }

    public class FileLoggerFactory : LoggerFactory
    {
        public override ILogger CreateLogger(LoggerType loggerType)
        {
            return new FileLogger();
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Console : {message}");
        }
    }

    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"File : {message}");
        }
    }

    public class FactoryMethodClient
    {


        public static void FactoryMethodMain(LoggerType loggerType)
        {
            ILogger logger;

            if (loggerType == LoggerType.Console)
            {
                logger = new ConsoleLogger();
            }
            else
            {
                logger = new FileLogger();
            }

            logger.Log("Log this message");
        }
    }
}