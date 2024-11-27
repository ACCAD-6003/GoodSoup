using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] Slider volSlider;
    void Start()
    {
        volSlider.value = AudioListener.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
