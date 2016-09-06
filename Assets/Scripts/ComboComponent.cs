using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Combo functions
 * 
 */
public class ComboComponent : MonoBehaviour {
    
    // UI text for displaying the current combo score
    public Text comboText;
    public int comboScore;

    // Slider for the time left to continue the combo
    public Slider comboResetSlider;
    public float comboResetTime;

    // Used to record the last combo
    float lastComboTime;

    void Start()
    {
        comboResetSlider.maxValue = comboResetTime;
        lastComboTime = Time.time;
    }

	void Update ()
    {
	    if (Time.time - lastComboTime > comboResetTime)
            ResetCombo();
        else
            comboResetSlider.value = Time.time - lastComboTime;
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
        comboResetSlider.value = comboResetSlider.maxValue;
        comboScore += value;
        comboText.text = "Combo : " + comboScore;
    }
}
