using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] Slider volSlider;
	[SerializeField] Canvas pauseCanvas;
	// Make sure GameLoader awake is called first in sort order
	private float _timeScale;
	private bool _paused = false;
    private void Awake()
	{
		AudioListener.volume = Globals.Volume;
		volSlider.value = Globals.Volume;
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (_paused) {
				Resume();
			}
            else
            {
                 Pause();
            }
		}
	}
	private void OnDestroy()
	{
		if (_paused) {
			Resume();
		}
	}
	private void Pause() {
		pauseCanvas.gameObject.SetActive(true);
		_timeScale = Time.timeScale;
		Time.timeScale = 0f;
		_paused = true;
	}
	public void Resume() 
	{ 
		Time.timeScale = _timeScale;
		pauseCanvas.gameObject.SetActive(false);
		_paused = false;
	}
	public void UpdateVolume() 
	{ 
		Globals.Volume = volSlider.value;
		AudioListener.volume = Globals.Volume;
	}
	public void QuitGame() {
		Application.Quit();
		// Will trigger save logic in GameSave
	}
}
