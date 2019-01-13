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
        Debug.Log("Scene name:  " + scene);
    }
}
