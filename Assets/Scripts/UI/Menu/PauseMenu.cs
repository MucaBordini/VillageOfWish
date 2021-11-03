using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void SaveGame()
    {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("SavedHealth", PlayerStats.getIstance().getHealthPoints());
        PlayerPrefs.SetInt("SavedPoints", PlayerStats.getIstance().getPoints());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        PlayerStats.getIstance().setHealth(PlayerPrefs.GetInt("SavedHealth"));
        PlayerStats.getIstance().setPoints(PlayerPrefs.GetInt("SavedPoints"));
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
