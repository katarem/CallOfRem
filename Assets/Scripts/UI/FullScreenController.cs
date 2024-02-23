using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FullScreenController : MonoBehaviour
{
    public Toggle fullScreenChecker;
    public TMP_Dropdown resDropdown;
    Resolution[] resolutions;

    void Start()
    {
        if(Screen.fullScreen)
        {
            fullScreenChecker.isOn = true;
        }
        else
        {
            fullScreenChecker.isOn = false;
        }
        checkResolution();
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void checkResolution()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
            
        }
        resDropdown.AddOptions(options);
        resDropdown.RefreshShownValue();

    }

    public void ChangeResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
