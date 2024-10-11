using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OnePlayer()
    {
        SceneManager.LoadScene(2);
    }

    public void TwoPlayers()
    {
        SceneManager.LoadScene(3);
    }

    public void ThreePlayers()
    {
        SceneManager.LoadScene(4);
    }

    public void FourPlayers()
    {
        SceneManager.LoadScene(5);
    }
}
