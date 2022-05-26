using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScisTooltipTr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MenuManager.chosen.CompareTag("Scissor"))
        {
            TooltipSystem.Show("Эффект: В этой стойке можно разыгрывать карты атакующей стойки. Карты атаки получают + 1 к урону. Не может переходить в атакующую стойку", "Яростная");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
