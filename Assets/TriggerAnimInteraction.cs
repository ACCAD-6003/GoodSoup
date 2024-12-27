using UnityEngine;

public class TriggerAnimInteraction : Interaction
{
	[SerializeField] Animator animator;
	[SerializeField] string triggerName;
	public override void DoAction()
	{
		animator.SetTrigger(triggerName);
		EndAction();
	}

	public override void LoadData(StoryDatastore data)
	{

	}

	public override void SaveData(StoryDatastore data)
	{

	}
}
