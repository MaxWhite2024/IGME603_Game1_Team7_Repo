using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FullScreenSetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown fullScreenOptions;

    private void Start()
    {
        fullScreenOptions.AddOptions(
            new List<string>
            {
                "Full Screen",
                "Borderless Windowed",
                "Windowed",
            }
        );
        UpdateDefaultSelection();
        fullScreenOptions.onValueChanged.AddListener(ChangeFullScreenMode);
    }

    private void UpdateDefaultSelection()
    {
        fullScreenOptions.value = Screen.fullScreenMode switch
        {
            FullScreenMode.ExclusiveFullScreen => 0,
            FullScreenMode.FullScreenWindow => 1,
            FullScreenMode.MaximizedWindow => 2,
            FullScreenMode.Windowed => 2,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void OnEnable()
    {
        UpdateDefaultSelection();
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