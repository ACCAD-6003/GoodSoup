using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ThoughtBubble : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public static Vector3 offset;
    public float hoverTransparency = 0.5f; 
    public float transitionDuration = 0.5f;
    public Image mainIcon, secondaryIcon, timerIcon;

    private RectTransform canvasRectTransform;
    private RectTransform imageRectTransform;
    private Image[] images;
    private Color[] originalColors; 

    private void Awake()
    {
        // Get the RectTransform of the canvas and the image
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        imageRectTransform = GetComponent<RectTransform>();

        // Get all Image components from the RectTransform and its children
        images = GetComponentsInChildren<Image>();

        // Store original colors of the images
        originalColors = new Color[images.Length];
        for (int i = 0; i < images.Length; i++)
        {
            originalColors[i] = images[i].color;
        }
    }

    void LateUpdate()
    {
        // Convert world position of character to viewport space
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(FindObjectOfType<ObjectInteraction>().Amber.position);

        // Convert viewport position to canvas space
        Vector2 canvasPos = new Vector2(
            ((viewportPos.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f)),
            ((viewportPos.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)));

        // Apply the offset
        canvasPos += new Vector2(offset.x, offset.y);

        // Apply the position to the image's RectTransform
        imageRectTransform.anchoredPosition = canvasPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change transparency of all images to hoverTransparency value
        StartCoroutine(ChangeTransparency(hoverTransparency));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change transparency of all images back to their original values
        StartCoroutine(ChangeTransparency(1f));
    }

    private IEnumerator ChangeTransparency(float targetAlpha)
    {
        // Calculate the step value for the transition based on the duration
        float step = Mathf.Abs((targetAlpha - images[0].color.a) / transitionDuration);

        // Transition each image's transparency
        for (float t = 0; t <= transitionDuration; t += Time.deltaTime)
        {
            for (int i = 0; i < images.Length; i++)
            {
                Color newColor = images[i].color;
                newColor.a = Mathf.MoveTowards(images[i].color.a, targetAlpha, step * Time.deltaTime);
                images[i].color = newColor;
            }
            yield return null;
        }

        // Ensure that all images reach exactly the targetAlpha
        for (int i = 0; i < images.Length; i++)
        {
            Color finalColor = images[i].color;
            finalColor.a = targetAlpha;
            images[i].color = finalColor;
        }
    }
}