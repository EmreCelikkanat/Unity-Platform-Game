using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainUI : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        menu.SetActive(true);
        settings.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        menu.SetActive(true);
        settings.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Settings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
    public void Back()
    {
        settings.SetActive(false);
        menu.SetActive(true);
    }
}
