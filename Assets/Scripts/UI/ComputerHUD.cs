using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ComputerHUD : SerializedMonoBehaviour
{
    public enum EmailState { NOTHING_CHANGED = 0, MEAN_EMAIL = 1, NICE_EMAIL = 2, MEAN_EMAIL_CONFIRMED = 3, NICE_EMAIL_CONFIRMED = 4, EMAIL_SENT = 5 }
    public Dictionary<EmailState, GameObject> EmailScreens;
}
