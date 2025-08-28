using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusBar : MonoBehaviour, IStatusBars
{
    private float maxValue = 1;
    public float currValue = 1;

    public Image imageFilled;
    public TextMeshProUGUI textInfor;

    public void Init(float max, float value = 0)
    {
        maxValue = max;
        currValue = value;
    }

    public void Zero()
    {
        currValue = 0;
        SetValue(currValue);
    }

    public void Full()
    {
        currValue = maxValue;
        SetValue(currValue);
    }
  

    public bool SetValue(float value)
    {
        currValue = value;
        currValue = Mathf.Clamp(currValue, 0, maxValue); // đảm bảo không vượt quá
        if (imageFilled) imageFilled.fillAmount = currValue / maxValue;
        if (textInfor) textInfor.text = currValue.ToString() + "/" + maxValue.ToString();

        return currValue > 0;
    }
}
