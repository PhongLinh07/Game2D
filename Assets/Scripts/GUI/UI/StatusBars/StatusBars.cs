using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusBar : MonoBehaviour, IStatusBars
{
    public float maxValue = 1;
    public float currValue = 1;

    public Image imageFilled;
    public TextMeshProUGUI textInfor;

    public Canvas canvas;
    
    public void Zero()
    {
        currValue = 0;
        UpdateStatusBar();
    }

    public void Full()
    {
        currValue = maxValue;
        UpdateStatusBar();
    }
    public bool Add(int amount)
    {
        canvas.enabled = true;  // Hiện
        currValue += amount;
        currValue = Mathf.Clamp(currValue, 0, maxValue); // đảm bảo không vượt quá
        UpdateStatusBar();
        return currValue > 0.0f;
    }

    public void UpdateStatusBar()
    {
        if (imageFilled) imageFilled.fillAmount = currValue / maxValue;

        if (textInfor) textInfor.text = currValue.ToString() + "/" + maxValue.ToString();
    }
}
