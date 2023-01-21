using TMPro;
using UnityEngine;

namespace Ui
{
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

        public GameObject charIcon;
    }
}
