using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel; // Панель настроек (перетащить в Inspector)
    private bool isMuted = false;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Показать панель настроек
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Скрыть панель настроек
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        Debug.Log("Звук " + (isMuted ? "выключен" : "включен"));
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Игра закрыта");
    }
}
