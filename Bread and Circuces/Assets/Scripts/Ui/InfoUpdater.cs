using System;
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
                CIP.charInfoHoplo,
                CIP.charInfoDim,
                CIP.charInfoThraex,
                CIP.charInfoVeles
            };

            foreach (var charInfo in heroes)
            {
                if (gameObject.tag != charInfo.charTag)
                {
                    charInfo.cards.SetActive(false);
                    if ((!MenuManager.team.Contains(charInfo.charObj.tag)))
                        charInfo.charObj.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 0.0f);
                    continue;
                }

                /*MenuManager.chosen = charInfo.charObj;
                MenuManager.charInfo = charInfo;*/
                MenuManager.chosen = charInfo.charObj;
                MenuManager.charInfo = charInfo;
                MenuManager.chosenButton = charInfo.button;
                MenuManager.Instance.ChangeChoiceButton();

                if (MenuManager.team.Count < 2)
                {
                    MenuManager.left = charInfo.leftPosition;
                    MenuManager.right = charInfo.rightPosition;
                    /*                    MenuManager.chosen = charInfo.charObj;
                                        MenuManager.charInfo = charInfo;
                                        MenuManager.chosenButton = charInfo.button; */
                    

                    charInfo.charObj.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    if (MenuManager.Instance.activePosition == "left")
                    {
                        charInfo.charObj.transform.GetChild(1).transform.localPosition = charInfo.leftPosition;
                    }
                    else if (!MenuManager.team.Contains(charInfo.charObj.tag))
                    {
                        charInfo.charObj.transform.GetChild(1).transform.localPosition = charInfo.rightPosition;
                    }
                }



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
