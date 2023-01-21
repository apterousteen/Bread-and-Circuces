using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardInfoScript : MonoBehaviour
    {
        public CardController CC;

        public Image Logo;
        public TextMeshProUGUI Name, Descsription;
        public bool highlighted = false;
        public float previousY;

        public void ShowCardInfo()
        {
            Logo.sprite = CC.Card.Logo;
            Logo.preserveAspect = true;
            Name.text = CC.Card.Name;
            Descsription.text = CC.Card.Description;
        }

        public void ManacostRefresh()
        {
            //Manacost.text = CC.Card.Manacost.ToString();
        }

        //public void HiglightManaAvaliability(int currentMana)
        //{
        //    GetComponent<CanvasGroup>().alpha = currentMana >= CC.Card.Manacost ? 1 : .5f;
        //}


        //public void HiglightTypeCard(bool typeCard_Attack )
        //{
        //    if(typeCard_Attack)
        //        AttackCard.SetActive(true);
        //    else
        //        DefenseCard.SetActive(false);
        //    /*
        //    if (card.Type == Card.CardType.Attack)
        //        CardInfo.HiglightTypeCard(true);
        //    else
        //        CardInfo.HiglightTypeCard(false);
        //    */
        //}

        public void HiglightCard(bool highlight)
        {
            //<CanvasGroup>().alpha = highlight ? 1 : .5f;
            var outline = GetComponentInChildren<Outline>();
            outline.enabled = highlight;
            //if (highlight && !highlighted)
            //{
            //    previousY = transform.position.y;
            //    transform.position += new Vector3(0, 0.4f, 0);
            //}
            //else if (highlighted && !highlight)
            //{
            //    //transform.position = new Vector3(transform.position.x, previousY, transform.position.z);
            //    transform.position -= new Vector3(0, 0.4f, 0);

            //    Debug.Log("Changed card position");
            //}
            //highlighted = highlight;
        }
    }
}
