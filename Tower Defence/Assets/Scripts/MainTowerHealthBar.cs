using DefaultNamespace;
using TMPro;
using UnityEngine;

public class MainTowerHealthBar : BaseHealthBar
{
    [SerializeField] private TMP_Text text;
    public override void UpdateHealthBar(float currentValue, float maxValue)
    {
        if(image != null)
            image.fillAmount = currentValue / maxValue;
        if(text != null)
            text.text = currentValue.ToString();
    }
}