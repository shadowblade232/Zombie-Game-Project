using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetQuality : MonoBehaviour
{
    private Dropdown qualityDropdown;

    public void Start()
    {
        qualityDropdown = GetComponent<Dropdown>();
    }
    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
