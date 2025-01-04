using UnityEngine;
using UnityEngine.SceneManagement;

public class DevExitLOL : MonoBehaviour
{
	private string konamiCode = "uuddlrlrba";
	private string inputBuffer = "";
	private bool codeEntered = false;

	void Update()
	{
		if (!codeEntered)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow)) inputBuffer += "u";
			if (Input.GetKeyDown(KeyCode.DownArrow)) inputBuffer += "d";
			if (Input.GetKeyDown(KeyCode.LeftArrow)) inputBuffer += "l";
			if (Input.GetKeyDown(KeyCode.RightArrow)) inputBuffer += "r";
			if (Input.GetKeyDown(KeyCode.B)) inputBuffer += "b";
			if (Input.GetKeyDown(KeyCode.A)) inputBuffer += "a";

			if (inputBuffer == konamiCode)
			{
				codeEntered = true;
				Debug.Log("Konami Code Entered! Now press a number key.");
			}
			else if (!konamiCode.StartsWith(inputBuffer))
			{
				inputBuffer = "";
			}
		}
		else
		{
			for (int i = 0; i <= 9; i++)
			{
				if (Input.GetKeyDown(KeyCode.Alpha0 + i))
				{
					JumpEnding((Ending)i);
				}
			}

			if (Input.GetKeyDown(KeyCode.Minus))
			{
				JumpEnding((Ending) 10);
			}
		}
	}
	private void JumpEnding(Ending ending)
	{
		StoryDatastore.Instance.ChosenEnding.Value = ending;
		SceneManager.LoadScene("Endings");
	}
}
