using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] string _sceneName;
	void Start()
    {
        SceneManager.LoadScene(_sceneName);
	}
}
