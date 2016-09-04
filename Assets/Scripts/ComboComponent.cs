using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboComponent : MonoBehaviour {

    public Text comboText;
    public int comboScore;

    public float comboResetTime;

    float lastComboTime;
    	
	// Update is called once per frame
	void Update ()
    {
	    if (Time.time - lastComboTime > comboResetTime)
        {
            ResetCombo();
        }
    }

    public void ResetCombo()
    {
        comboScore = 0;
    }

    void AddCombo()
    {
        lastComboTime = Time.time;
        comboScore += 1;
        comboText.text = "Combo : " + comboScore;
    }
}
