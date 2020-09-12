using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sourav.Engine.Core.DebugRelated
{
    public class D
    {
        public static void Log(object message)
        {
            #if LOG || UNITY_EDITOR
            Debug.Log(message);
            #endif
        }

        public static void Log(object message, Object context)
        {
            #if LOG || UNITY_EDITOR
            Debug.Log(message, context);
            #endif
        }

        public static void LogError(object message)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogError(message);
            #endif
        }

        public static void LogError(object message, Object context)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogError(message, context);
            #endif
        }

        public static void LogException(Exception exception)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogException(exception);
            #endif
        }
        
        public static void LogException(Exception exception, Object context)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogException(exception, context);
            #endif
        }

        public static void LogWarning(object message)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogWarning(message);
            #endif
        }
        
        public static void LogWarning(object message, Object context)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogWarning(message, context);
            #endif
        }

        public static void LogAssertion(object message)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogAssertion(message);
            #endif
        }
        
        public static void LogAssertion(object message, Object context)
        {
            #if LOG || UNITY_EDITOR
            Debug.LogAssertion(message, context);
            #endif
        }
    }
}
