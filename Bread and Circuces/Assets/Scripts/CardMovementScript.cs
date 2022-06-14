using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CardController CC;

    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent;
    public bool isDraggable, isClickable, canBePlayed, isPlayerLastCard;
    private bool selected;

    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        isClickable = selected = canBePlayed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isClickable)
            return;
        transform.localPosition += new Vector3(0, 0.4f, 0.4f);
        //transform.localPosition += new Vector3(0, 0.4f, 0);
        transform.localScale += new Vector3(0.4f, 0.4f, 0);

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isClickable || Input.GetMouseButtonUp(0))
            return;
        transform.localPosition -= new Vector3(0, 0.4f, 0.4f);
        //transform.localPosition -= new Vector3(0, 0.4f, 0);
        transform.localScale -= new Vector3(0.4f, 0.4f, 0);
    }

    public void OnBeginDrag(PointerEventData eventData) //Как только НАЧНЕМ двигать объект-сработает все что внутри метода(по сути будет работать за один кадр)
    {
        if (!canBePlayed || isClickable)
            return;
        else DefaultParent = transform.parent;
        offset = transform.position - MainCamera.WorldToScreenPoint(eventData.position); // Хранит в себе значение отступа центра карты от места карты по которой нажали(без этого карта будет дергаться )
        

        isDraggable = /*GameManagerScript.Instance.IsPlayerTurn &&*/
                     DefaultParent.GetComponent<DropPlaceScript>().Type == FieldType.SelfHand &&
                     canBePlayed;
       
        if (!isDraggable)
        {
            Debug.Log("Can't be dragged");
            return;
        }
        else 
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(DefaultParent.parent);
        }
    }

    public void OnDrag(PointerEventData eventData) //Будет работать все время пока мы ДВИГАЕМ карту
    {
        if (!isDraggable)
            return;
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos; //Присваиваем текущую позицию карту
    }

    public void OnEndDrag(PointerEventData eventData) //Как только ОТПУСТИМ объект-сработает все что внутри метода(по сути будет работать за один кадр)
    {
        if (!isDraggable)
            return;
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        UiController.Instance.playerHandSize.text = GameManagerScript.Instance.PlayerHand.childCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClickable)
            return;
        var discardWindow = FindObjectOfType<DiscardWindow>();
        Debug.Log("Card clicked");
        if (selected)
        {
            selected = false;
            discardWindow.DeselectCard(gameObject);
        }
        else
        {
            selected = true;
            discardWindow.SelectCard(gameObject);
        }
    }
}
