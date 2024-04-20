using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public Image YellowStar;
    Vector3 _savedScale;
    private void Awake()
    {
        _savedScale = transform.localScale;
        YellowStar.transform.localScale = Vector3.zero;
    }
    public void EnableStar() {
        YellowStar.transform.localScale = _savedScale;
    }
}