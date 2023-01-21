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
                    //charInfo.charObj.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
                    charInfo.charObj.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
                    charInfo.charObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    charInfo.charObj.transform.GetChild(1).GetComponent<Outline>().enabled = false;
                    continue;
                }

                MenuManager.chosen = charInfo.charObj;
                MenuManager.Instance.ChangeChoiceButton();

                //charInfo.charObj.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                charInfo.charObj.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                charInfo.charObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                charInfo.charObj.transform.GetChild(1).GetComponent<Outline>().enabled = true;

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
                    GameObject.Find("panel_defense").GetComponentInChildren<TextMeshProUGUI>().text = "??????";
                }
                else
                {
                    GameObject.Find("panel_defense").GetComponent<Image>().sprite = MenuManager.Instance.defence;
                    GameObject.Find("panel_defense").GetComponentInChildren<TextMeshProUGUI>().text = "???????";
                }
            }

        }
    }
}
