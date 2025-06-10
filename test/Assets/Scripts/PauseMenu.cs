using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        // Проверка на нажатие ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC нажата. Текущее состояние паузы: " + isPaused);
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // Продолжить игру
    public void Resume()
    {
        Debug.Log("Продолжить игру");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Поставить игру на паузу
    public void Pause()
    {
        Debug.Log("Пауза активирована");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Перейти в главное меню
    public void LoadMainMenu()
    {
        Debug.Log("Переход в главное меню");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Выйти из игры
    public void QuitGame()
    {
        Debug.Log("Игра закрыта");
        Application.Quit();
    }
}
