using System;
using System.Collections;
using UnityEngine;

public class StoryData<T> where T : class
{
    T _value;

    public T Value { 
        get 
        {
            return _value;
        }
        set
        {
            if (value != _value)
            {
                return;
            }

            T oldValue = _value;
            _value = value;
            Changed?.Invoke(oldValue, _value);
        }
    }

    [SerializeField]
    public event Action<T, T> Changed;
        
}