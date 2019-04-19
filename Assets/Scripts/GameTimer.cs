using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    private Image fillImg;
    public static float timeAmt = 120;
    private float time;
    public Text timeText;
    public GameObject timer;

    void Start()
    {
        fillImg = this.GetComponent<Image>();
        time = 0.0f;
    }
    void Update()
    {
        time += Time.deltaTime;


        fillImg.fillAmount = time / timeAmt;
        timeText.text = Mathf.Round(time).ToString();
    }
}
