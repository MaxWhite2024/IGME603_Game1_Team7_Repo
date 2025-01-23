using UnityEngine;
using UnityEngine.UI;

public class CameraInverterSetting : MonoBehaviour
{
    [SerializeField] private Toggle xAxisToggle;
    [SerializeField] private Toggle yAxisToggle;
    
    private Player_Movement _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<Player_Movement>();
        xAxisToggle.onValueChanged.AddListener(ToggleXAxis);
        yAxisToggle.onValueChanged.AddListener(ToggleYAxis);
        UpdateToggle();
    }

    private void OnEnable()
    {
        UpdateToggle();
    }

    private void UpdateToggle()
    {
        if (!_playerController) return;

        xAxisToggle.isOn = _playerController.isCameraXInverted;
        yAxisToggle.isOn = _playerController.isCameraYInverted;
    }

    private void ToggleXAxis(bool value)
    {
        if (!_playerController) return;

        _playerController.isCameraXInverted = value;
        UpdateToggle();
    }
    private void ToggleYAxis(bool value)
    {
        if (!_playerController) return;
        
        _playerController.isCameraYInverted = value;
        UpdateToggle();
    }
}