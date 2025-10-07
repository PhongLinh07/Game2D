using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IStatusBars
{
    public void Init(float maxValue, float value = 0);
    public void Zero();
    public void Full();
    public bool SetValue(float value);

}

