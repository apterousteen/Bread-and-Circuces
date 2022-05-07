using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public CardController CC;

    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent;
    public bool IsDraggable;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
    }
    public void OnBeginDrag(PointerEventData eventData) //Как только НАЧНЕМ двигать объект-сработает все что внутри метода(по сути будет работать за один кадр)
    {
        offset = transform.position - MainCamera.WorldToScreenPoint(eventData.position); // Хранит в себе значение отступа центра карты от места карты по которой нажали(без этого карта будет дергаться )
        DefaultParent = transform.parent;

        transform.SetParent(DefaultParent.parent);

        IsDraggable = GameManagerScript.Instance.IsPlayerTurn &&
            (DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SELF_HAND &&
            GameManagerScript.Instance.CurrentGame.Player.Mana >= CC.Card.Manacost);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        if (!IsDraggable)
            return;

        if (CC.Card.IsSpell)
            GameManagerScript.Instance.HighlightTargets(CC, true);
    }

    public void OnDrag(PointerEventData eventData) //Будет работать все время пока мы ДВИГАЕМ карту
    {
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos; //Присваиваем текущую позицию карту
    }

    public void OnEndDrag(PointerEventData eventData) //Как только ОТПУСТИМ объект-сработает все что внутри метода(по сути будет работать за один кадр)
    {
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
