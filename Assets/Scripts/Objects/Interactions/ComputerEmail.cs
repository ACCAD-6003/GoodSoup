using Assets.Scripts.Objects.Interactions;
using Assets.Scripts.UI;
using System;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.UI.UIElements;
using static ComputerHUD;

public class ComputerEmail : Interaction
{
    [SerializeField] GameObject PCUI, EmailHUDElement;
    [SerializeField] ComputerHUD hud;
    private ObjectInteraction popUp;
    private float _timeScale;
    private void Awake()
    {
        popUp = FindObjectOfType<ObjectInteraction>();
    }
    private void Update()
    {
        if (StoryDatastore.Instance.AwaitingEmailReply.Value)
        {
            if (EmailHUDElement.activeInHierarchy)
            {
                EmailHUDElement.SetActive(false);
            }
            if ((DateTime.Now - StoryDatastore.Instance.EmailSentTime.Value).TotalSeconds >= Globals.SECONDS_BETWEEN_EMAIL_REPLIES)
            {
                StoryDatastore.Instance.AwaitingEmailReply.Value = false;
            }
        }
        else if (!EmailHUDElement.activeInHierarchy)
        {
            EmailHUDElement.SetActive(true);
        }
    }
    public override void LoadData(StoryDatastore data)
    {
    }
    private void DisplayEmailScreen() {
        foreach (var element in hud.EmailScreens) {
            element.Value.SetActive(false);
        }
        if (StoryDatastore.Instance.AwaitingEmailReply.Value)
        {
            hud.EmailScreens[EmailState.EMAIL_SENT].SetActive(true);
            return;
        }
        hud.EmailScreens[StoryDatastore.Instance.EmailState.Value].SetActive(true);
    }
    public void SendEmail(int email) {
        StoryDatastore.Instance.EmailState.Value = (EmailState) email;
        StoryDatastore.Instance.AwaitingEmailReply.Value = true;
        StoryDatastore.Instance.EmailSentTime.Value = DateTime.Now;
        DisplayEmailScreen();
    }

    public void CloseScreen() {
        popUp.PopUpOpened = false;
        PCUI.SetActive(false);
        Time.timeScale = _timeScale;
        EndAction();
    }
    public override void DoAction()
    {
        PCUI.SetActive(true);
        DisplayEmailScreen();
		_timeScale = Time.timeScale;
		Time.timeScale = 0f;
        popUp.PopUpOpened = true;

    }

    public override void SaveData(StoryDatastore data)
    {

    }
}
