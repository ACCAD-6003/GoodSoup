using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] Slider volSlider;
	[SerializeField] Canvas pauseCanvas;
	// Make sure GameLoader awake is called first in sort order
	private float _timeScale;
	public static bool Paused = false;
    private void Awake()
	{
		AudioListener.volume = Globals.Volume;
		volSlider.value = Globals.Volume;
	}
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Paused) {
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
		if (Paused) {
			Resume();
		}
	}
	private void Pause() {
		pauseCanvas.gameObject.SetActive(true);
		_timeScale = Time.timeScale;
		Time.timeScale = 0f;
		Paused = true;
	}
	public void Resume() 
	{ 
		Time.timeScale = _timeScale;
		pauseCanvas.gameObject.SetActive(false);
		Paused = false;
	}
	public void ReturnToMenu()
	{
		GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
		StoryDatastore.Instance.DestroyStoryData();
		foreach (GameObject obj in dontDestroyObjects)
		{
			Destroy(obj);
		}
		SceneManager.LoadScene("Title Screen");
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
