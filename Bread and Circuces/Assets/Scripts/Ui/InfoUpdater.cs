using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
                charInfo.charObj.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 0.6f);
                charInfo.charObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                continue;
            }

            MenuManager.chosen = charInfo.charObj;

            charInfo.charObj.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            charInfo.charObj.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);

            CIP.charName.text = charInfo.charName;
            CIP.health.text = charInfo.health.ToString();
            CIP.moveDistance.text = charInfo.moveDistance.ToString();
            CIP.attackReachDistance.text = charInfo.attackReachDistance.ToString();
            CIP.info.text = charInfo.info;
            charInfo.cards.SetActive(true);
            CIP.cardPanel = charInfo.cards;
        }
    }
}
