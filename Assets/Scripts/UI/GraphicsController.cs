using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GraphicsController : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    public int quality;
    void Start()
    {
        quality = PlayerPrefs.GetInt("qualityNumber");
        dropdown.value = quality;
        setQuality();
    }

    public void setQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("qualityNumber", dropdown.value);
        quality = dropdown.value;
    }
}
