using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyLabel : MonoBehaviour
{
    private TextMeshProUGUI valueLabel;
    // Start is called before the first frame update
    void Start()
    {
        foreach (TextMeshProUGUI label in GetComponentsInChildren<TextMeshProUGUI>())
            if (label.name == "Value")
                valueLabel = label;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value)
    {
        valueLabel.text = value.ToString();
    }
}
