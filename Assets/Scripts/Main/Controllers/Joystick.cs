using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public Transform knob;
    [HideInInspector]
    public Vector2 direction;

    RectTransform rectTransform;
    PlayerController m_playerController;
    Vector2 inputPosition;

    bool inputDown;
    int pointerId;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetCurrentController(PlayerController playerController)
    {
        m_playerController = playerController;
    }

    private void Update()
    {
        if (!inputDown) return;
        float radius = rectTransform.rect.width / 2f;
        knob.position = inputPosition;
        Vector2 point = new Vector2(knob.localPosition.x, knob.localPosition.y);
        if (point.magnitude > radius)
        {
            knob.localPosition = point.normalized * radius;
        }
        direction = point.normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        inputPosition = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerId = eventData.pointerId;
        inputDown = true;
        inputPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != pointerId) return;
        inputDown = false;
        direction = Vector2.zero;
        knob.localPosition = Vector3.zero;
        pointerId = -1;
    }

    
}
