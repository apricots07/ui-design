
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;

    private void Awake()
    {
        if (settingsPanel == null)
        {
            Debug.LogError("Settings Panel is not assigned!");
            return;
        }

        settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ToggleSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    private void Update()
    {
        if (settingsPanel != null &&
            settingsPanel.activeSelf &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseSettings();
        }
    }
}
