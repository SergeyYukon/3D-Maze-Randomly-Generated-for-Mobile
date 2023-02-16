using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using System;



public class OnScreenStickCustom : UnityEngine.InputSystem.OnScreen.OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler   
{     
    [InputControl(layout = "Vector2")]       
    [SerializeField] private string m_ControlPath;
    [SerializeField] private float limitRange;
    [SerializeField] private float speedMoveToStart;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private Vector2 startPositionFromParentZero;
    private Vector3 startPosition;
    private Vector2 delta;
    private bool isPhaseEnded;

    protected override string controlPathInternal       
    {      
        get => m_ControlPath;       
        set => m_ControlPath = value;      
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    private void Start()
    {
        startPosition = rectTransform.position;
    }

    private void FixedUpdate()
    {
        if (isPhaseEnded)
        {
            SmoothMoveToStart();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPhaseEnded = false;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out startPositionFromParentZero); // ���������� ��������� ������� ������������ 0 ��������� �������
    }

    public void OnDrag(PointerEventData eventData)      
    {
        Vector2 currentPositionFromParentZero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out currentPositionFromParentZero); // ���������� ������� ������� ������������ 0 ��������� �������
        delta = currentPositionFromParentZero - startPositionFromParentZero;
        delta = Vector2.ClampMagnitude(delta, limitRange); // ������������ ����� ������� �� ������
        rectTransform.position = startPosition + (Vector3)delta;
        Vector2 newPos = new Vector2(delta.x / limitRange, delta.y / limitRange); 
        SendValueToControl(newPos); // ���������� ������ ����������� �� ���������� �� -1 �� 1, ����� ������������ �� ����� ��� �������� �������, ��������
    }    
       
    public void OnPointerUp(PointerEventData eventData)       
    {
        // rectTransform.position = startPosition;
        // SendValueToControl(Vector2.zero);     // ��� ���������� ����, ������� ����������� ������������ �� �����, � ������ ����������� ����������� ����������� � 0
        isPhaseEnded = true;
    }

    private void SmoothMoveToStart()
    {
        rectTransform.position = Vector3.MoveTowards(rectTransform.position, startPosition, speedMoveToStart * Time.fixedDeltaTime);
        if (delta.x > 0.1f) delta.x -= speedMoveToStart * Time.fixedDeltaTime;
        if (delta.x < 0.1f) delta.x += speedMoveToStart * Time.fixedDeltaTime;
        if (delta.y > 0.1f) delta.y -= speedMoveToStart * Time.fixedDeltaTime;
        if (delta.y < 0.1f) delta.y += speedMoveToStart * Time.fixedDeltaTime;
        Vector2 startPos = new Vector2(delta.x / limitRange, delta.y / limitRange);
        SendValueToControl(startPos);
        if (Mathf.Abs(rectTransform.position.x - startPosition.x) < 0.1f)
        {
            rectTransform.position = startPosition;
            isPhaseEnded = false;
        }
    }
}
