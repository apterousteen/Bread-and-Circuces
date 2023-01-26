using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class InfoUpdater : MonoBehaviour
    {
        public CharInfoPanel CIP;
        public GameObject Popup;

        public void OnMouseDown()
        {
            Debug.Log(this.gameObject.tag + " Was Clicked");

            var heroes = new List<CharInfo>
            {
                CIP.charInfoRet,
                CIP.charInfoMurm,
                CIP.charInfoSkis,
                CIP.charInfoHoplo
            };

            foreach (var charInfo in heroes)
            {
                if (gameObject.tag != charInfo.charTag)
                {
                    charInfo.cards.SetActive(false);
                    charInfo.charObj.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
                    
                    continue;
                }

                MenuManager.chosen = charInfo.charObj;
                MenuManager.chosenButton = charInfo.button;
                MenuManager.Instance.ChangeChoiceButton();

                charInfo.charObj.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                CIP.charIcon.GetComponent<Image>().sprite = charInfo.charIcon;
                CIP.charName.text = charInfo.charName;
                CIP.health.text = charInfo.health.ToString();
                CIP.moveDistance.text = charInfo.moveDistance.ToString();
                CIP.attackReachDistance.text = charInfo.attackReachDistance.ToString();
                CIP.info.text = charInfo.info;
                charInfo.cards.SetActive(true);
                CIP.cardPanel = charInfo.cards;

                if (MenuManager.chosen.CompareTag("Scissor"))
                {
                    GameObject.Find("panel_defense").GetComponent<Image>().sprite = MenuManager.Instance.rage;
                    GameObject.Find("panel_defense").GetComponentInChildren<TextMeshProUGUI>().text = "ярость";
                }
                else
                {
                    GameObject.Find("panel_defense").GetComponent<Image>().sprite = MenuManager.Instance.defence;
                    GameObject.Find("panel_defense").GetComponentInChildren<TextMeshProUGUI>().text = "ќборона";
                }
            }

        }
    }
}
