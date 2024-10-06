using System;
using UnityEngine;



public static class DebugUtils
{
    public static string GameObjectNamePretty(GameObject gameObject)
    {
        return $"<color=cyan>[{gameObject.name}]>></color>";
    }
}