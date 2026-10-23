using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Canvas), typeof(GraphicRaycaster), typeof(Button))]
public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float hoverScale = 1.15f;
    [SerializeField] float scaleSpeed = 14f;
    [SerializeField] int hoverSortingOrder = 10;

    Canvas canvas;
    Button button;
    Vector3 targetScale = Vector3.one;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        button = GetComponent<Button>();
    }

    void OnDisable()
    {
        transform.localScale = Vector3.one;
        targetScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable) return;   // for the cards that don't do anything
        targetScale = Vector3.one * hoverScale;
        canvas.overrideSorting = true;
        canvas.sortingOrder = hoverSortingOrder;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = Vector3.one;
        canvas.overrideSorting = false;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }
}
