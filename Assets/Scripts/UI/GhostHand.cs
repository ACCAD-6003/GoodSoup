using UnityEngine;
using UnityEngine.UI;

public class GhostHand : MonoBehaviour
{
    public Image ghostImage;
    private void Awake()
    {
        //Cursor.visible = false;
    }
    void Update()
    {
        //Vector3 mousePos = Input.mousePosition;
        //mousePos += new Vector3(ghostImage.rectTransform.rect.width/2, -ghostImage.rectTransform.rect.height/2, 0);

        //ghostImage.transform.position = mousePos;
    }
}
