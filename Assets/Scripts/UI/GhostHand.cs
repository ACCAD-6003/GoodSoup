using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostHand : MonoBehaviour
{
    public List<Sprite> frames;
    private int currFrameIndex = 0;
    [SerializeField] Image image;
    [SerializeField] int FPS;
    
    private void Awake()
    {
        StartCoroutine(PlayGhostHandAnimation());
        Cursor.visible = false;
        DontDestroyOnLoad(transform.parent);
    }
    IEnumerator PlayGhostHandAnimation() {
        image.sprite = frames[currFrameIndex];
        yield return new WaitForSeconds(1f / FPS);
        currFrameIndex++;
        if (currFrameIndex >= frames.Count) {
            currFrameIndex = 0;
        }
        StartCoroutine(PlayGhostHandAnimation());
    }
    void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos += new Vector3(image.rectTransform.rect.width/1.9f, -image.rectTransform.rect.height/2.5f, 0);
        image.transform.position = mousePos;
        if (Cursor.visible) {
            Cursor.visible = false;
        }
    }
}
