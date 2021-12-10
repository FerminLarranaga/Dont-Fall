using System.Collections;
using UnityEngine;

public class DropdownController : MonoBehaviour
{

    public RectTransform container;

    private bool isOpen = false;

    public void HandleDD()
    {
        isOpen = !isOpen;
        StartCoroutine(HandleBehaviour());
    }

    IEnumerator HandleBehaviour()
    {
        while (isOpen ? container.localScale.y < 1-0.01 : container.localScale.y > 0 + 0.01)
        {
            Vector3 scale = container.localScale;
            scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.unscaledDeltaTime * 12);
            container.localScale = scale;
            yield return null;
        }
        container.localScale = new Vector3(1, isOpen? 1 : 0, 1);
    }
}
