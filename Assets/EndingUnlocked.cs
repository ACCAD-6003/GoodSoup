using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUnlocked : MonoBehaviour
{
    [SerializeField] GameObject title, endings, book;
    public bool firstTime = false;
    private void Start()
    {
        firstTime = Globals.FirstTitleScreen;
        if (!Globals.FirstTitleScreen)
        {
            title.SetActive(false);
            endings.SetActive(true);
        }
        else {
            Globals.FirstTitleScreen = false;
        }
    }
}
