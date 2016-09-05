using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour {

    public Slider healthSlider;
    public Image damageImage;

    public void SetSlider(float value)
    {
        healthSlider.value = value;
    }
}
