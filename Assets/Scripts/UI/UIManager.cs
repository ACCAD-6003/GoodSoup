using System.Collections;
using UnityEngine;
using static Assets.Scripts.UI.UIElements;

namespace Assets.Scripts.UI
{
    internal class UIManager : MonoBehaviour
    {
        [SerializeField] ThoughtBubble bubble;
        [SerializeField] UIElements icons;
        private Coroutine bubbleCoroutine;

        private static UIManager instance;
        public static UIManager Instance { get => instance; private set { instance = value; } }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            ClearBubble();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ClearBubble()
        {
            if (bubbleCoroutine != null)
            {
                bubbleCoroutine = null;
            }
            bubble.mainIcon.sprite = null;
            bubble.secondaryIcon.sprite = null;
            bubble.gameObject.SetActive(false);
        }
        private void DisplayIcon(BubbleIcon icon) {
            bubble.gameObject.SetActive(true);
            bubble.mainIcon.sprite = icons.Bubbles[icon].icon;
        }

        public void DisplaySimpleBubbleTilInterrupted(BubbleIcon icon)
        {
            if (bubbleCoroutine != null)
            {
                StopCoroutine(bubbleCoroutine);
                bubbleCoroutine = null;
            }
            ClearBubble();
            DisplayIcon(icon);
        }

        public void DisplaySimpleBubbleForSeconds(BubbleIcon icon, float seconds)
        {
            if (bubbleCoroutine != null)
            {
                StopCoroutine(bubbleCoroutine);
                bubbleCoroutine = null;
            }
            ClearBubble();

            bubbleCoroutine = StartCoroutine(DisplayBubbleForSecondsCoroutine(icon, seconds));
        }

        private IEnumerator DisplayBubbleForSecondsCoroutine(BubbleIcon icon, float seconds)
        {
            DisplayIcon(icon);

            yield return new WaitForSeconds(seconds);

            if (bubbleCoroutine != null)
            {
                ClearBubble();
                bubbleCoroutine = null;
            }
        }
    }
}
