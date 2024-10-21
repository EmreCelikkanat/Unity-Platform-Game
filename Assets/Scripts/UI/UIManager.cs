using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField]private AudioClip clip;
    [SerializeField] private GameObject winScreen;

    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        gameOverScreen .SetActive(false); 
        winScreen .SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
                Pause(false);
            else
                Pause(true);
        }
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(clip);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Scene restarted");
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Pause(bool status)
    {
        pauseMenu.SetActive(status);
        Time.timeScale = System.Convert.ToInt32(!status);
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void WinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
