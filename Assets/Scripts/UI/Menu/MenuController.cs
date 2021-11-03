using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void carregar()
    {
        SceneManager.LoadScene(1);
    }

    public void sair()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        PlayerStats.getIstance().setHealth(PlayerPrefs.GetInt("SavedHealth"));
        PlayerStats.getIstance().setPoints(PlayerPrefs.GetInt("SavedPoints"));
    }

}
