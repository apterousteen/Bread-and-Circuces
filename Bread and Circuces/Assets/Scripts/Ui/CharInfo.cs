using UnityEngine;

namespace Ui
{
    public class CharInfo : MonoBehaviour
    {
        public GameObject charObj;
        public string charName;
        public int health;
        public int moveDistance;
        public int attackReachDistance;
        public string info;
        public string charTag;
        public GameObject cards;

        public Sprite charIcon;

        public GameObject button;
        public Vector3 leftPosition;
        public Vector3 rightPosition;
        public string slotPosition;
    }
}
