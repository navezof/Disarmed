using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour {

    public Slider healthSlider;
    public Image damageImage;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    bool bDamaged;

    void Update()
    {
        /*
        if (bDamaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        bDamaged = false;*/
    }

    public void SetDamaged(bool value)
    {
        bDamaged = value;
    }

    public void SetSlider(float value)
    {
        healthSlider.value = value;
    }
}
