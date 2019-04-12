using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static bool retryPressed;

    private void Start()
    {
        //LevelUnlock.unlock = true;
    }
    public void SceneTransition(string scene)
    {
        Debug.Log("Button pressed");
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void TurnOn(GameObject On)
    {
        On.SetActive(true);
    }

    public void TurnOff(GameObject Off)
    {
        Off.SetActive(false);
    }

    public void SceneUnlock(string scene)
    {
        //if (LevelSelector.unlock)
        //{
        //    SceneManager.LoadScene(scene);
        //}
    }

    public void RetryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
