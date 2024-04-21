using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemFix : MonoBehaviour
{
    void Start()
    {
        var eventSystems = FindObjectsOfType<EventSystem>();
        foreach (var eventSystem in eventSystems)
        {
            if (eventSystem.gameObject != this.gameObject) {
                Destroy(eventSystem);
            }
        }
    }
}
