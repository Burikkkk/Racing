using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    // Метод для выхода из игры
    public void ExitGame()
    {

        // Если игра запущена в редакторе, то остановить игру
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Если игра запущена как сборка, то закрыть приложение
        Application.Quit();
#endif
    }

    public void SimpleTrack()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void HardTrack()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
