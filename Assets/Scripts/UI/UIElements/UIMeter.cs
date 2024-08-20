using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeter : MonoBehaviour
{
    [SerializeField] private Color[] gradientCols;
    [SerializeField] private float[] gradientVals;
    [SerializeField] private float jumpThreshold = 0.05f;
    [SerializeField] private float jumpSmoothing = 0.015f;
    private Slider slider;
    private Animator shakeAnim;
    private float sliderVal;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        shakeAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(sliderVal - slider.value) < jumpThreshold)
            slider.value = sliderVal;
        else
            slider.value = Mathf.Lerp(slider.value, sliderVal, jumpSmoothing);

        shakeAnim.SetFloat("MeterVal", slider.value);
    }

    public void SetValues(float value, float rate)
    {
        sliderVal = value;
        UpdateColour(rate);
    }

    private void UpdateColour(float rate)
    {
        ColorBlock sliderCols = ColorBlock.defaultColorBlock;

        for (int i = 0; i < gradientVals.Length; i++)
        {
            if (i == gradientVals.Length - 1)
            {
                sliderCols.disabledColor = gradientCols[i];
                break;
            }

            if (rate < gradientVals[i + 1])
            {
                float t = (rate - gradientVals[i]) / (gradientVals[i + 1] - gradientVals[i]);
                sliderCols.disabledColor = Color.Lerp(gradientCols[i], gradientCols[i + 1], t);
                break;
            }
        }

        slider.colors = sliderCols;
    }
}
