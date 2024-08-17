using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public const int TOOTHPICKS = 0;
    public const int STRING = 1;
    public const int BUTTONS = 2;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private FloorButton[] floorButtons;
    [SerializeField] private int[] startingCurrency = new int[3];
    [SerializeField] private float[] startingProdution = new float[3];
    [SerializeField] private String[] floorStartingCosts;
    [SerializeField] private String[] floorCostIncreases;

    private float[] currencyVals = { 0, 0, 0 };
    private float[] currencyRates = { 0, 0, 0 };
    private float[,] floorCosts;
    private float[,] costIncreases;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < currencyVals.Length; i++)
            currencyVals[i] = startingCurrency[i];

        for (int i = 0; i < currencyRates.Length; i++)
            currencyRates[i] = startingProdution[i];

        floorCosts = new float[floorStartingCosts.Length, 3];
        costIncreases = new float[floorStartingCosts.Length, 3];

        for (int i = 0; i < floorStartingCosts.Length; i++)
        {
            String[] costStrings = floorStartingCosts[i].Split(char.Parse(","));

            for (int j = 0; j < 3; j++)
                floorCosts[i, j] = float.Parse(costStrings[j]);

            UpdatePriceLabels(i);

            costStrings = floorCostIncreases[i].Split(char.Parse(","));

            for (int j = 0; j < 3; j++)
                costIncreases[i, j] = float.Parse(costStrings[j]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            currencyVals[i] += currencyRates[i] * Time.deltaTime;

            uiManager.SetCurrencyValues(i, Mathf.FloorToInt(currencyVals[i]));
        }

        for (int i = 0; i < floorButtons.Length; i++)
        {
            floorButtons[i].SetAffordances(GetAffordances(i));
        }
    }

    private void UpdatePriceLabels(int floorID)
    {
        floorButtons[floorID].SetCostLabels(
                Mathf.FloorToInt(GetIntegerCost(floorID, 0)), 
                Mathf.FloorToInt(GetIntegerCost(floorID, 1)), 
                Mathf.FloorToInt(GetIntegerCost(floorID, 2)));
    }

    private int GetIntegerCost(int floorID, int currency)
    {
        return Mathf.FloorToInt(floorCosts[floorID, currency]);
    }

    private bool[] GetAffordances(int floorID)
    {
        return new bool[] {
            Mathf.FloorToInt(currencyVals[0]) >= GetIntegerCost(floorID, 0),
            Mathf.FloorToInt(currencyVals[1]) >= GetIntegerCost(floorID, 1),
            Mathf.FloorToInt(currencyVals[2]) >= GetIntegerCost(floorID, 2)
        };
    }

    private bool CheckAfford(int floorID)
    {
        return Mathf.FloorToInt(currencyVals[0]) >= GetIntegerCost(floorID, 0) &&
               Mathf.FloorToInt(currencyVals[1]) >= GetIntegerCost(floorID, 1) &&
               Mathf.FloorToInt(currencyVals[2]) >= GetIntegerCost(floorID, 2);
    }

    public bool PurchaseFloor(int floorID)
    {
        if (!CheckAfford(floorID)) return false;

        for (int i = 0; i < 3; i++)
        {
            currencyVals[i] -= GetIntegerCost(floorID, i);
            floorCosts[floorID, i] += costIncreases[floorID, i];
        }

        UpdatePriceLabels(floorID);

        return true;
    }

    public void SetCurrencyRate(int type, float rate)
    {
        currencyRates[type] = Mathf.Max(0, rate);
    }

    public void SetCurrencyVal(int type, float val)
    {
        currencyVals[type] = Mathf.Max(0, val);
    }

    public void ChangeCurrencyRate(int type, float rate)
    {
        currencyRates[type] = Mathf.Max(0, currencyRates[type] + rate);
    }

    public void ChangeCurrencyVal(int type, float val)
    {
        currencyVals[type] = Mathf.Max(0, currencyVals[type] + val);
    }

    public float GetCurrencyVal(int type)
    {
        return currencyVals[type];
    }

    public float GetCurrencyRate(int type)
    {
        return currencyRates[type];
    }
}
