using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupLabel : MonoBehaviour
{
    private TextMeshProUGUI valueLabel;
    private Button actionButton;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (TextMeshProUGUI label in GetComponentsInChildren<TextMeshProUGUI>())
            if (label.name == "Value")
                valueLabel = label;

        actionButton = GetComponentInChildren<Button>();

        SetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue(int value)
    {
        valueLabel.text = "x " + value.ToString();

        actionButton.interactable = value <= 0 ? false : true;
    }
}
