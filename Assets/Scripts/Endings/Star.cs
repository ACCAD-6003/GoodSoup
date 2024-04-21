using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image YellowStar;
    bool enabled = false;
    private void Awake()
    {
        if (enabled) {
            return;
        }
        YellowStar.transform.localScale = Vector3.zero;
    }
    public void EnableStar() {
        enabled = true;
        YellowStar.enabled = true;
        YellowStar.gameObject.SetActive(true);
        YellowStar.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}