using System;
using System.Collections.Generic;

public static class StaticRegistry
{
    public static Dictionary<Type, UnityEngine.Object> _register = new();

    public static void Add<T>(T obj) where T : UnityEngine.Object
    {
        var type = typeof(T);
        if (!_register.ContainsKey(type))
        {
            _register.Add(type, obj);
        }
    }

    public static void Remove<T>(T obj) where T : UnityEngine.Object
    {
        var type = typeof(T);
        if(_register.TryGetValue(type, out var unregister) && unregister == obj)
        {
            _register.Remove(type);
        }
    }

    public static void Clear()
    {
        _register.Clear();
    }

    public static T Find<T>() where T : UnityEngine.Object
    {
        return _register.TryGetValue(typeof(T), out var obj) ? obj as T : null;
    }
}
