using System;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.UI.UIElements;

namespace Assets.Scripts.UI
{
    internal class UIManager : MonoBehaviour
    {
        [SerializeField] ThoughtBubble bubble;
        [SerializeField] UIElements icons;
		[SerializeField] AudioSource src;
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
            var iconData = icons.Bubbles[icon];
			bubble.mainIcon.sprite = iconData.icon;
            var clips = iconData.audioClips.Count;
            if (clips > 0) 
            {
				var clip = iconData.audioClips[UnityEngine.Random.Range(0, clips)];
				if (clip != null)
				{
					src.PlayOneShot(clip);
				}
				else
				{
					Debug.LogWarning($"Null audio clip for icon: {icon}");
				}
			}
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

            Debug.Log(icon.ToString() + " ::: " + seconds.ToString());

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
