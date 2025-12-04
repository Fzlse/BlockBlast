using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
   public Button OpenSettingsButton;
    public Button CloseSettingsButton;

    private void Awake()
    {
        // Ensure references exist and set a safe initial state
        if (OpenSettingsButton != null)
        {
            OpenSettingsButton.gameObject.SetActive(true);
            OpenSettingsButton.interactable = true;
        }

        if (CloseSettingsButton != null)
        {
            CloseSettingsButton.gameObject.SetActive(false);
            CloseSettingsButton.interactable = false;
        }
    }

    public void SettingsOpened()
    {
        if (OpenSettingsButton == null || CloseSettingsButton == null)
        {
            Debug.LogWarning("SettingsButton: Button references are missing.");
            return;
        }

        OpenSettingsButton.gameObject.SetActive(false);
        CloseSettingsButton.gameObject.SetActive(true);
        CloseSettingsButton.interactable = true;
    }

    public void SettingsClosed()
    {
        if (OpenSettingsButton == null || CloseSettingsButton == null)
        {
            Debug.LogWarning("SettingsButton: Button references are missing.");
            return;
        }

        OpenSettingsButton.gameObject.SetActive(true);
        CloseSettingsButton.gameObject.SetActive(false);
        OpenSettingsButton.interactable = true;
    }
}