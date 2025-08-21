using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IStatusBars
{
    public void Zero();
    public void Full();
    public bool Add(int mount); // true if > 0 || false <= 0
    public void UpdateStatusBar();

}

