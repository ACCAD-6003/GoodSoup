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
    private StoryData<EmailState> _state;
    private StoryData<DateTime> _emailSentTime;
    private StoryData<bool> _awaitingResponse;
    private void Update()
    {
        if (_awaitingResponse.Value)
        {
            if (EmailHUDElement.activeInHierarchy)
            {
                EmailHUDElement.SetActive(false);
            }
            if ((DateTime.Now - _emailSentTime.Value).TotalSeconds >= Globals.SECONDS_BETWEEN_EMAIL_REPLIES)
            {
                _awaitingResponse.Value = false;
            }
        }
        else if (!EmailHUDElement.activeInHierarchy)
        {
            EmailHUDElement.SetActive(true);
        }
    }
    public override void LoadData(StoryDatastore data)
    {
        _emailSentTime = data.EmailSentTime;
        _state = data.EmailState;
        _awaitingResponse = data.AwaitingEmailReply;
    }
    private void DisplayEmailScreen() {
        foreach (var element in hud.EmailScreens) {
            element.Value.SetActive(false);
        }
        if (_awaitingResponse.Value)
        {
            hud.EmailScreens[EmailState.EMAIL_SENT].SetActive(true);
            return;
        }
        hud.EmailScreens[_state.Value].SetActive(true);
    }
    public void SendEmail(int email) {
        _state.Value = (EmailState) email;
        _awaitingResponse.Value = true;
        _emailSentTime.Value = DateTime.Now;
        DisplayEmailScreen();
    }

    public void CloseScreen() {
        PCUI.SetActive(false);
        EndAction();
    }
    public override void DoAction()
    {
        PCUI.SetActive(true);
        DisplayEmailScreen();
    }

    public override void SaveData(StoryDatastore data)
    {

    }
}
