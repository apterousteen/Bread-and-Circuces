using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharInfoPanel : MonoBehaviour
{
    public static CharInfoPanel instance;
 
    public CharInfo charInfoRet;
    public CharInfo charInfoMurm;
    public CharInfo charInfoSkis;
    public CharInfo charInfoHoplo;

    public TextMeshProUGUI charName;
    public TextMeshProUGUI health;
    public TextMeshProUGUI moveDistance;
    public TextMeshProUGUI attackReachDistance;
    public TextMeshProUGUI info;

    public GameObject cardPanel;
}
