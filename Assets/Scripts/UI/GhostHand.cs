using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostHand : MonoBehaviour
{
    public List<Texture2D> frames;
    private int currFrameIndex = 0;
    [SerializeField] int FPS;
    
    private void Awake()
    {
        if (FindObjectsOfType<GhostHand>().Length > 1) {
            Destroy(gameObject);
        }
        StartCoroutine(PlayGhostHandAnimation());
        DontDestroyOnLoad(transform);
    }
    IEnumerator PlayGhostHandAnimation() {
        Cursor.SetCursor(frames[currFrameIndex], new Vector2(16, 21), CursorMode.ForceSoftware);
        yield return new WaitForSeconds(1f / FPS);
        currFrameIndex++;
        if (currFrameIndex >= frames.Count) {
            currFrameIndex = 0;
        }
        StartCoroutine(PlayGhostHandAnimation());
    }
}
