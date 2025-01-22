using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FullScreenSetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown fullScreenOptions;

    private void Start()
    {
        var currentFullScreenMode = Screen.fullScreenMode;

        fullScreenOptions.AddOptions(
            new List<string>
            {
                "Full Screen",
                "Borderless Windowed",
                "Windowed",
            }
        );
        fullScreenOptions.value = currentFullScreenMode switch
        {
            FullScreenMode.ExclusiveFullScreen => 0,
            FullScreenMode.FullScreenWindow => 1,
            FullScreenMode.MaximizedWindow => 2,
            FullScreenMode.Windowed => 2,
            _ => throw new ArgumentOutOfRangeException()
        };
        fullScreenOptions.onValueChanged.AddListener(ChangeFullScreenMode);
    }

    private void ChangeFullScreenMode(int option)
    {
        Screen.fullScreenMode = option switch
        {
            0 => FullScreenMode.ExclusiveFullScreen,
            1 => FullScreenMode.FullScreenWindow,
            2 => FullScreenMode.Windowed,
            3 => FullScreenMode.Windowed,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}