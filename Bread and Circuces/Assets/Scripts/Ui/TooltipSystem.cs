using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public Tooltip tooltip;

    public void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.tooltip.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        current.tooltip.SetText(content, header);  
        current.tooltip.gameObject.SetActive(true);
    }

    public static void ShowSpecial(string content, string header = "")
    {
        current.tooltip.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
