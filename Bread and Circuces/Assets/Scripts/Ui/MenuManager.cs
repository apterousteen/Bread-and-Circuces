using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public CharInfoPanel CIP;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnMouseDown()
    {
        Debug.Log(this.gameObject.tag + " Was Clicked");

        if (this.gameObject.tag == "Retiarius")
        {
            CIP.charName.text = CIP.charInfoRet.charName;
            CIP.health.text = CIP.charInfoRet.health.ToString();
            CIP.moveDistance.text = CIP.charInfoRet.moveDistance.ToString();
            CIP.attackReachDistance.text = CIP.charInfoRet.attackReachDistance.ToString();
            CIP.info.text = CIP.charInfoRet.info;
        }

        if (this.gameObject.tag == "Murmillo")
        {
            CIP.charName.text = CIP.charInfoMurm.charName;
            CIP.health.text = CIP.charInfoMurm.health.ToString();
            CIP.moveDistance.text = CIP.charInfoMurm.moveDistance.ToString();
            CIP.attackReachDistance.text = CIP.charInfoMurm.attackReachDistance.ToString();
            CIP.info.text = CIP.charInfoMurm.info;
        }

        if (this.gameObject.tag == "Scissor")
        {
            CIP.charName.text = CIP.charInfoSkis.charName;
            CIP.health.text = CIP.charInfoSkis.health.ToString();
            CIP.moveDistance.text = CIP.charInfoSkis.moveDistance.ToString();
            CIP.attackReachDistance.text = CIP.charInfoSkis.attackReachDistance.ToString();
            CIP.info.text = CIP.charInfoSkis.info;
        }

        if (this.gameObject.tag == "Hoplomachus")
        {
            CIP.charName.text = CIP.charInfoHoplo.charName;
            CIP.health.text = CIP.charInfoHoplo.health.ToString();
            CIP.moveDistance.text = CIP.charInfoHoplo.moveDistance.ToString();
            CIP.attackReachDistance.text = CIP.charInfoHoplo.attackReachDistance.ToString();
            CIP.info.text = CIP.charInfoHoplo.info;
        }
    }
}
