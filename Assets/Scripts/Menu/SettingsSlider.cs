using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour {
    public float min = 0;
    public float max = 1;

    public string setting = "";

    public string displayName = "Setting";
    public string valueFormat = "F1";

    [Header("Components")]
    public Slider slider;
    public Text label;
    public Text value;

    void Start()
    {
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = Settings.GetFloat(setting);

        label.text = displayName;
    }
    void OnEnable()
    {
        slider.value = Settings.GetFloat(setting);
    }

    void Update()
    {
        value.text = slider.value.ToString(valueFormat);
        Settings.SetFloat(setting, slider.value);
    }
}
