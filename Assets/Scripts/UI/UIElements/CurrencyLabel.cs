using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyLabel : MonoBehaviour
{
    private TextMeshProUGUI valueLabel;
    private TextMeshProUGUI capLabel;

    // Start is called before the first frame update
    void Start()
    {
        foreach (TextMeshProUGUI label in GetComponentsInChildren<TextMeshProUGUI>())
            if (label.name == "Value")
                valueLabel = label;
            else if (label.name == "Cap")
                capLabel = label;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value, int cap)
    {
        valueLabel.text = value.ToString();
        capLabel.text = cap.ToString();
    }
}
