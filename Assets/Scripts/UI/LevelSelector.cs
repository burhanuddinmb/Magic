using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static bool unlock;

    public Button[] levelButtons;
    public Sprite lockSprite;
    
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        levelReached = 6;
        levelButtons[levelReached].GetComponentInChildren<Text>().text = levelReached.ToString();
        for (int i = 1; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].GetComponent<Image>().sprite = lockSprite;
                levelButtons[i].GetComponent<Button>().interactable = false;
                levelButtons[i].GetComponentInChildren<Text>().text = "";
            } 
        }
    }
    void Update()
    {
        
    }
    public void OnClicked(int level)
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }
}
