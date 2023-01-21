using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class MurmTooltipTr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string header;
        public string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (MenuManager.chosen != null && MenuManager.chosen.CompareTag("Murmillo"))
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
