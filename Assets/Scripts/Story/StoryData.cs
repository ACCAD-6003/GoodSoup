using System;
using System.Collections;
using UnityEngine;
public interface IStoryData {
    public Type GetDataType();
}

[Serializable]
public class StoryData<T> : IStoryData
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

    public Type GetDataType() {
        return typeof(T);
    }

    override
    public string ToString()
    {
        return "" + _value;
    }
        
}