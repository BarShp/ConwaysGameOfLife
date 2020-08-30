using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI liveCellsText;
    [SerializeField] private TextMeshProUGUI generationText;
    [SerializeField] private TextMeshProUGUI paused;


    void Start()
    {
        UpdateLiveCells(0);
        UpdateGeneration(0);
    }

    public void UpdateLiveCells(int amount)
    {
        liveCellsText.text = GetLiveCellsMsg(amount);
    }

    public void UpdateGeneration(int generation)
    {
        generationText.text = GetGenerationMsg(generation);
    }

    public void SetPauseView(bool isPlaying)
    {
        paused.gameObject.SetActive(!isPlaying);
    }

    private string GetLiveCellsMsg(int amount) => $"Live Cells - {amount}";
    private string GetGenerationMsg(int generation) => $"Generation - {generation}";
}
