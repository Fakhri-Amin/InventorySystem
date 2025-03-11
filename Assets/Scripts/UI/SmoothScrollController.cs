using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SmoothScrollController : MonoBehaviour, IScrollHandler
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float smoothTime = 0.08f; // Adjust for more/less smoothness

    private Vector2 targetNormalizedPosition;
    private Coroutine smoothScrollCoroutine;

    private void Start()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();

        targetNormalizedPosition = scrollRect.normalizedPosition;
    }

    public void OnScroll(PointerEventData eventData)
    {
        // Calculate new scroll position
        targetNormalizedPosition += eventData.scrollDelta * new Vector2(0, 0.1f);
        targetNormalizedPosition.y = Mathf.Clamp01(targetNormalizedPosition.y);

        // Stop previous coroutine if it's still running
        if (smoothScrollCoroutine != null)
            StopCoroutine(smoothScrollCoroutine);

        // Start smooth scrolling coroutine
        smoothScrollCoroutine = StartCoroutine(SmoothScroll());
    }

    private IEnumerator SmoothScroll()
    {
        Vector2 start = scrollRect.normalizedPosition;
        float elapsed = 0f;

        while (elapsed < smoothTime)
        {
            elapsed += Time.unscaledDeltaTime;
            scrollRect.normalizedPosition = Vector2.Lerp(start, targetNormalizedPosition, elapsed / smoothTime);
            yield return null;
        }

        scrollRect.normalizedPosition = targetNormalizedPosition;
    }
}
