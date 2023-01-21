using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class ScisTooltipTr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string header;
        public string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (MenuManager.chosen != null && MenuManager.chosen.CompareTag("Scissor"))
            { 
                TooltipSystem.Show(content, header);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }
    }
}
