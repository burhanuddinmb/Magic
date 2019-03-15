using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    private Image fillImg;
    public static float timeAmt = 60;
    private float time;
    public Text timeText;
    public GameObject timer;

    void Start()
    {
        fillImg = this.GetComponent<Image>();
        time = timeAmt;
    }
    void Update()
    {
        if (time > 0)
        {
            
            time -= Time.deltaTime;

            
            fillImg.fillAmount = time / timeAmt;
            timeText.text = Mathf.Round(time).ToString();

            Debug.Log("Time.deltaTime:  " + Time.timeScale);
        }
        if (time < 0)
        {
            timer.SetActive(false);
        }
    }

   
}
