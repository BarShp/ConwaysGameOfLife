using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_InputField gridW;
    [SerializeField] private TMP_InputField gridH;
    [SerializeField] private TextMeshProUGUI stepsRateText;
    [SerializeField] private Toggle randomize;

    public void ResetGame()
    {
        int gridWValue = ValidateNormalGridInputs(gridW, 128, 150);
        int gridHValue = ValidateNormalGridInputs(gridH, 96, 150);
        var gridSize = new Vector2Int(gridWValue, gridHValue);
        gameManager.ResetGameOfLife(gridSize, randomize.isOn);
    }

    public void UpdateStepsRateText(float stepsRate)
    {
        stepsRateText.text = GetStepsRateText(stepsRate);
    }

    private int ValidateNormalGridInputs(TMP_InputField gridInput, int defaultValue, int maxValue)
    {
        if (gridInput.text == "")
        {
            gridInput.text = defaultValue.ToString();
            return defaultValue;
        }
        int value = Mathf.Clamp(int.Parse(gridInput.text), 1, maxValue);
        gridInput.text = value.ToString();
        return value;
    }

    private string GetStepsRateText(float stepsRate) => $"Steps Rate {String.Format("{0:0.##}", stepsRate)}";
}
