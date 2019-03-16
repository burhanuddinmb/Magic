using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool isClicked;
    private bool isPaused;
    // Use this for initialization
    void Start()
    {
        isClicked = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ControlAction()
    {
        isClicked = !isClicked;
        if (isClicked)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

    public void PauseTheGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Debug.Log("Game paused");
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Game resumed");
            Time.timeScale = 1;
        }
    }
}
