using UnityEngine;

namespace Ui
{
    public class HealthBar : MonoBehaviour
    {
        public GameObject bar;

        public void SetHealth(float health, float maxHealth)
        { 
            bar.transform.localScale = new Vector3((health / maxHealth), bar.transform.localScale.y, bar.transform.localScale.z);
            bar.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.black, new Color(0.7830189f, 0.1318247f, 0.04062834f, 1f), health / maxHealth);
        }
    }
}
