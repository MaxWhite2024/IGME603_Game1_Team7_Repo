using System.Linq;
using TMPro;
using UnityEngine;

public class ResolutionSetting : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionOptions;

    private void Start()
    {
        resolutionOptions.AddOptions(
            Screen.resolutions
                .Select(it => it.ToString())
                .ToList()
        );
        UpdateDefaultSelection();

        resolutionOptions.onValueChanged.AddListener(SetResolution);
    }

    private void UpdateDefaultSelection()
    {
        resolutionOptions.value = Screen.resolutions.ToList().IndexOf(Screen.currentResolution);
    }

    private void OnEnable()
    {
        UpdateDefaultSelection();
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