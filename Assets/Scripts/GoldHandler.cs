using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoldHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Transform pickupParent;
    [SerializeField] GameObject winCanvas;

    int gold = 0;

    private void Start()
    {
        UpdateGoldUI();
    }

    public void IncreaseGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();

        if (pickupParent.childCount == 1)
        {
            ProcessWin();
        }
    }

    private void ProcessWin()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
    }

    private void UpdateGoldUI()
    {
        goldText.text = "Gold: " + gold.ToString();
    }
}
