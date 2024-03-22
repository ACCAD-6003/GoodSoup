using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class StoryData<T>
{
    [SerializeField]
    T _value;

    public T Value { 
        get 
        {
            return _value;
        }
        set
        {
            if (value.Equals(_value))
            {
                return;
            }

            T oldValue = _value;
            _value = value;
            Changed?.Invoke(oldValue, _value);
        }
    }

    public event Action<T, T> Changed;

    public StoryData(T initialValue)
    {
        _value = initialValue;
    }

    override
    public string ToString()
    {
        return "" + _value;
    }
        
}