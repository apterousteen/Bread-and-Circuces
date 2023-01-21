using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class MoveTooltipTr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string header;
        public string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipSystem.Show(content, header);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }
    }
}