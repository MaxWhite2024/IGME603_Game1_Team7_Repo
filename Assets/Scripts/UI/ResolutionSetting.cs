using System.Linq;
using TMPro;
using UnityEngine;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionOptions;

    private void Start()
    {
        var currentResolution = Screen.currentResolution;

        resolutionOptions.AddOptions(
            Screen.resolutions
                .Select(it => it.ToString())
                .ToList()
        );
        resolutionOptions.value = Screen.resolutions.ToList().IndexOf(currentResolution);

        resolutionOptions.onValueChanged.AddListener(SetResolution);
    }

    private void SetResolution(int option)
    {
        var selection = Screen.resolutions[option];
        var currentFullScreenMode = Screen.fullScreenMode;
        Screen.SetResolution(
            selection.width,
            selection.height,
            currentFullScreenMode,
            selection.refreshRateRatio
        );
    }
}