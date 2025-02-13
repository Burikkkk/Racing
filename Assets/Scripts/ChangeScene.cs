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

    // ����� ��� ������ �� ����
    public void ExitGame()
    {

        // ���� ���� �������� � ���������, �� ���������� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� ���� �������� ��� ������, �� ������� ����������
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
