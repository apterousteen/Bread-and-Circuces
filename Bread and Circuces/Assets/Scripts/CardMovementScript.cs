using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    public CardController CC;

    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent;
    public bool IsDraggable;
    public bool IsClickable;
    public bool CanBePlayed;
    private bool selected;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        IsClickable = false;
        selected = false;
        CanBePlayed = false;
    }

    public void OnBeginDrag(PointerEventData eventData) //Как только НАЧНЕМ двигать объект-сработает все что внутри метода(по сути будет работать за один кадр)
    {
        if (IsClickable)
            return;
        offset = transform.position - MainCamera.WorldToScreenPoint(eventData.position); // Хранит в себе значение отступа центра карты от места карты по которой нажали(без этого карта будет дергаться )
        DefaultParent = transform.parent;

        transform.SetParent(DefaultParent.parent);

        IsDraggable = GameManagerScript.Instance.IsPlayerTurn &&
                     DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SelfHand &&
                     CanBePlayed;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (!IsDraggable)
            return;
    }

    public void OnDrag(PointerEventData eventData) //Будет работать все время пока мы ДВИГАЕМ карту
    {
        if (!IsDraggable)
            return;

        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos; //Присваиваем текущую позицию карту
    }

    public void OnEndDrag(PointerEventData eventData) //Как только ОТПУСТИМ объект-сработает все что внутри метода(по сути будет работать за один кадр)
    {
        if(!IsDraggable)
            return;

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClickable)
            return;
        var discardWindow = FindObjectOfType<DiscardWindow>();
        Debug.Log("Card clicked");
        if (selected)
            discardWindow.DeselectCard(gameObject);
        else discardWindow.SelectCard(gameObject);
    }
}
