using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextListener : MonoBehaviour
{
    private TMP_Text valueText;
    public void Init()
    {
        valueText = GetComponent<TMP_Text>();
    }

    public void ChangeValue(float value)
    {
        valueText.text = value.ToString("0.00");
    }
}
