using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Combo functions
 * 
 */
public class ComboComponent : MonoBehaviour {

    public Text comboText;
    public int comboScore;

    public Slider comboResetSlider;
    public float comboResetTime;

    float lastComboTime;

    void Start()
    {
        comboResetSlider.maxValue = comboResetTime;
        lastComboTime = Time.time;
    }

	void Update ()
    {
	    if (Time.time - lastComboTime > comboResetTime)
        {
            ResetCombo();
        }
        else
        {
            comboResetSlider.value = Time.time - lastComboTime;
        }
    }

    public void ResetCombo()
    {
        comboScore = 0;
        comboResetSlider.value = 0;
        comboText.text = "Combo : " + comboScore;
    }

    public void AddCombo(int value)
    {
        lastComboTime = Time.time;
        comboScore += value;
        comboText.text = "Combo : " + comboScore;
    }
}
