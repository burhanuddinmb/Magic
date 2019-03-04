using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneTransition(string scene)
    {
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
}
