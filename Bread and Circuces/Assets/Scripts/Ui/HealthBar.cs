using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public GameObject bar;

    /*    public void SetHealth(float health, float maxHealth)
        {
            Slider.value = health;
            Slider.maxValue = maxHealth;

            Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
        }
    */

    public void SetHealth(float health, float maxHealth)
    { 
        bar.transform.localScale = new Vector3((health / maxHealth), bar.transform.localScale.y, bar.transform.localScale.z);
    }

    /*    private void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
    */
}
