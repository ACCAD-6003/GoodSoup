using UnityEngine;

public class Fadeout : MonoBehaviour
{
    [SerializeField] GameObject fadeOutPanel;
	public void RunAnim()
	{
		fadeOutPanel.SetActive(true);
	}
}
