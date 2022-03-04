using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoSingleton<GameController>
{
    public int currentSceneIndex;

    public void RestartGame()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        currentSceneIndex++;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
